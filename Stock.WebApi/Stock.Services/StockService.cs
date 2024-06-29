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

        public async Task<IEnumerable<StockDto>> GetAllStocksAsync()
        {
            IEnumerable<StockDto> stocks = await this.repository.AllReadonly<Stock>()
                .Select(s => s.ToStockDto())
                .ToListAsync();

            return stocks;
        }
    }
}
