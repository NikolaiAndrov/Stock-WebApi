namespace Stock.WebApi.DtoModels.Stock
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.StockValidation;
    using static Common.ApplicationErrorMessages;

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

        [Range(PageMinValue, PageMaxValue)]
        public int Page { get; set; }

        [Range(ItemsPerPageMinValue, ItemsPerPageMaxValue)]
        public int ItemsPerPage { get; set; }
    }
}
