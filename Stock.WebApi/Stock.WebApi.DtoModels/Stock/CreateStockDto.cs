namespace Stock.WebApi.DtoModels.Stock
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.StockValidation;

    public class CreateStockDto
    {
        [Required]
        [StringLength(SymbolMaxLength, MinimumLength = SymbolMinLength)]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [StringLength(CompanyNameMaxLength, MinimumLength = CompanyNameMinLength)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [StringLength(IndustryMaxLength, MinimumLength = IndustryMinLength)] 
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(typeof(decimal), PurchaseMinValue, PurchaseMaxValue)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(typeof(decimal), LastDivMinValue, LastDivMaxValue)]
        public decimal LastDiv { get; set; }

        [Required]
        [Range(MarketCapMinValue, MarketCapMaxValue)]
        public long MarketCap { get; set; }

    }
}
