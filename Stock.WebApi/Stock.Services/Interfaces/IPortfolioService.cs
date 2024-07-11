namespace Stock.Services.Interfaces
{
    using WebApi.DtoModels.Stock;

    public interface IPortfolioService
    {
        Task<IEnumerable<StockDto>> GetUserPortfolioAsync(string userId);

        Task<bool> IsPortfolioAlreadyAddedAsync(string userId, int stockId);

        Task CreatePortfolioAsync(string userId, int stockId);

        Task DeletePortfolioAsync(string userId, int stockId);
    }
}
