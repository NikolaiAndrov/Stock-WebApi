﻿namespace Stock.WebApi.DtoModels.Stock
{
    public class StockQueryModel
    {
        public string? Symbol { get; set; }

        public string? CompanyName { get; set; }

        public string? Industry { get; set; }
    }
}