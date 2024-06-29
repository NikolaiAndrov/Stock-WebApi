namespace Stock.Services.Interfaces
{
    using WebApi.DtoModels.Stock;

    public interface IStockService
    {
        Task<IEnumerable<StockDto>> GetAllStocksAsync();
    }
}
