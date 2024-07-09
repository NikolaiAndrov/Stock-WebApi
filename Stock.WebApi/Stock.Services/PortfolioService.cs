namespace Stock.Services
{
    using Microsoft.EntityFrameworkCore;
    using Stock.Data.Common.Repository;
    using Stock.Data.Models;
    using Stock.Services.Interfaces;
    using Stock.WebApi.DtoModels.Stock;
    using Stock.WebApi.Infrastructure.Extensions;

    public class PortfolioService : IPortfolioService
    {
        private readonly IRepository repository;

        public PortfolioService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<StockDto>> GetUserPortfolioAsync(string userId)
        {
            IEnumerable<StockDto> stockDtos = await this.repository.AllReadonly<Portfolio>()
                .Where(p => p.ApplicationUserId == userId)
                .Include(p => p.Stock.Comments)
                .Select(p => p.Stock.ToStockDto())
                .ToListAsync();

            return stockDtos;
        }
    }
}
