namespace Stock.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Interfaces;
    using Data.Common.Repository;
    using Data.Models;
    using WebApi.DtoModels.Stock;
    using WebApi.Infrastructure.Extensions;

    public class StockService : IStockService
    {
        private readonly IRepository repository;

        public StockService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<StockDto> CreateStockAsync(CreateStockDto createStockDto)
        {
            Stock stock = createStockDto.ToStock();

            await this.repository.AddAsync<Stock>(stock);
            await this.repository.SaveChangesAsync();

            return stock.ToStockDto();
        }

        public async Task DeleteAsync(int id)
        {
            Stock? stock = await this.repository.GetByIdAsync<Stock>(id);
            this.repository.Delete<Stock>(stock!); 
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDto>> GetAllStocksAsync(StockQueryModel stockQueryModel)
        {
            IQueryable<Stock> stocksQuery = this.repository.AllReadonly<Stock>()
                .Include(s => s.Comments)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(stockQueryModel.Symbol))
            {
                string wildCard = $"%{stockQueryModel.Symbol.ToLower()}%";

                stocksQuery = stocksQuery
                    .Where(s => EF.Functions.Like(s.Symbol, wildCard));
            }

            if (!string.IsNullOrWhiteSpace(stockQueryModel.CompanyName))
            {
                string wildCard = $"%{stockQueryModel.CompanyName.ToLower()}%";

                stocksQuery = stocksQuery
                    .Where(s => EF.Functions.Like(s.CompanyName, wildCard));
            }

            if (!string.IsNullOrWhiteSpace(stockQueryModel.Industry))
            {
                string wildCard = $"%{stockQueryModel.Industry.ToLower()}%";

                stocksQuery = stocksQuery
                    .Where(s => EF.Functions.Like(s.Industry, wildCard));
            }

            if (!string.IsNullOrWhiteSpace(stockQueryModel.SortBy))
            {
                if (stockQueryModel.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocksQuery = stockQueryModel.IsDescending ? stocksQuery.OrderByDescending(s => s.Symbol) : stocksQuery.OrderBy(s => s.Symbol);
                }
                else if (stockQueryModel.SortBy.Equals("Purchase", StringComparison.OrdinalIgnoreCase))
                {
                    stocksQuery = stockQueryModel.IsDescending ? stocksQuery.OrderByDescending(s => s.Purcase) : stocksQuery.OrderBy(s => s.Purcase);
                }
                else if (stockQueryModel.SortBy.Equals("Last Divident", StringComparison.OrdinalIgnoreCase))
                {
                    stocksQuery = stockQueryModel.IsDescending ? stocksQuery.OrderByDescending(s => s.LastDiv) : stocksQuery.OrderBy(s => s.LastDiv);
                }
            }

            IEnumerable<StockDto> stocks = await stocksQuery
                .Select(s => s.ToStockDto())
                .ToListAsync();

            return stocks;
        }

        public async Task<StockDto?> GetStockByIdAsync(int id)
        {
            Stock? stock = await this.repository.AllReadonly<Stock>()
                .Where(s => s.Id == id)
                .Include(s => s.Comments)
                .FirstOrDefaultAsync();

            if (stock == null)
            {
                return null;
            }

            StockDto stockDto = stock.ToStockDto();

            return stockDto;
        }

        public async Task<bool> IsStockExistingByIdAsync(int id)
        {
            bool isExisting = await this.repository.AllReadonly<Stock>()
                .AnyAsync(s => s.Id == id);

            return isExisting;
        }

        public async Task<StockDto?> UpdateAsync(int id, UpdateStockDto updateStockDto)
        {
            Stock? stock = await this.repository.GetByIdAsync<Stock>(id);

            if(stock == null)
            {
                return null;
            }

            stock.Symbol = updateStockDto.Symbol;
            stock.CompanyName = updateStockDto.CompanyName;
            stock.Industry = updateStockDto.Industry;
            stock.Purcase = updateStockDto.Purchase;
            stock.LastDiv = updateStockDto.LastDiv;
            stock.MarketCap = updateStockDto.MarketCap;

            await this.repository.SaveChangesAsync();

            return stock.ToStockDto();
        }
    }
}
