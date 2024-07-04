namespace Stock.WebApi.DtoModels.Stock
{
    public class StockQueryModel
    {
        public StockQueryModel()
        {
            this.Page = 1;
            this.ItemsPerPage = 3;
        }

        public string? Symbol { get; set; }

        public string? CompanyName { get; set; }

        public string? Industry { get; set; }

        public string? SortBy { get; set; }

        public bool IsDescending { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
