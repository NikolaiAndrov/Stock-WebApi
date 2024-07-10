namespace Stock.Services.Interfaces
{
    using Stock.WebApi.DtoModels.Stock;

    public interface IPortfolioService
    {
        Task<IEnumerable<StockDto>> GetUserPortfolioAsync(string userId);

        Task<bool> IsPortfolioAlreadyAddedAsync(string userId, int stockId);

        Task CreatePortfolioAsync(string userId, int stockId);
    }
}
