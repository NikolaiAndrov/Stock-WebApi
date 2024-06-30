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

        public async Task<int> CreateStockReturnIdAsync(CreateStockDto createStockDto)
        {
            Stock stock = createStockDto.ToStock();

            await this.repository.AddAsync<Stock>(stock);
            await this.repository.SaveChangesAsync();

            return stock.Id;
        }

        public async Task<IEnumerable<StockDto>> GetAllStocksAsync()
        {
            IEnumerable<StockDto> stocks = await this.repository.AllReadonly<Stock>()
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
