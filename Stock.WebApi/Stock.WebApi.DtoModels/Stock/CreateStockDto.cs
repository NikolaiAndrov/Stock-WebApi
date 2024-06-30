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

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public long MarketCap { get; set; }

    }
}
