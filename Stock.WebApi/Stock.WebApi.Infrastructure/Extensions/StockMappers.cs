﻿namespace Stock.WebApi.Infrastructure.Extensions
{
    using Stock.WebApi.DtoModels.Stock;
    using Stock.Data.Models;

    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            StockDto stockDto = new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                Purchase = stock.Purcase,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap
            };

            return stockDto;
        }

        public static Stock ToStock(this CreateStockDto createStockDto)
        {
            Stock stock = new Stock
            {
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Industry = createStockDto.Industry,
                Purcase = createStockDto.Purchase,
                LastDiv = createStockDto.LastDiv,
                MarketCap = createStockDto.MarketCap
            };

            return stock;
        }
    }
}
