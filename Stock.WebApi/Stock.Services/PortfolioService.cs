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

        public async Task CreatePortfolioAsync(string userId, int stockId)
        {
            Portfolio portfolio = new Portfolio
            {
                ApplicationUserId = userId,
                StockId = stockId
            };

            await this.repository.AddAsync<Portfolio>(portfolio);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeletePortfolioAsync(string userId, int stockId)
        {
            Portfolio? portfolio = await this.repository.All<Portfolio>()
                .Where(p => p.ApplicationUserId == userId && p.StockId == stockId)
                .FirstOrDefaultAsync();

            this.repository.Delete<Portfolio>(portfolio!);
            await this.repository.SaveChangesAsync();
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

        public async Task<bool> IsPortfolioAlreadyAddedAsync(string userId, int stockId)
        {
            bool isExisting = await this.repository.AllReadonly<Portfolio>()
                .AnyAsync(p => p.ApplicationUserId == userId && p.StockId == stockId);

            return isExisting;
        }
    }
}
