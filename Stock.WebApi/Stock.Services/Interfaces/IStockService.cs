﻿namespace Stock.Services.Interfaces
{
    using WebApi.DtoModels.Stock;

    public interface IStockService
    {
        Task<IEnumerable<StockDto>> GetAllStocksAsync();

        Task<StockDto?> GetStockByIdAsync(int id);

        Task<int> CreateStockReturnIdAsync(CreateStockDto createStockDto);

        Task<StockDto?> UpdateAsync(int id, UpdateStockDto updateStockDto);
    }
}
