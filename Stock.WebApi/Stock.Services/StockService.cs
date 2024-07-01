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

        public async Task<IEnumerable<StockDto>> GetAllStocksAsync()
        {
            IEnumerable<StockDto> stocks = await this.repository.AllReadonly<Stock>()
                .Include(s => s.Comments)
                .Select(s => s.ToStockDto())
                .ToListAsync();

            return stocks;
        }

        public async Task<StockDto?> GetStockByIdAsync(int id)
        {
            Stock? stock = await this.repository.GetByIdAsync<Stock>(id);

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
