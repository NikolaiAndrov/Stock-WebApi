namespace Stock.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.StockValidation;

    public class Stock
    {
        public Stock()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(SymbolMaxLength)]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(CompanyNameMaxLength)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(IndustryMaxLength)]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = DecimalColumnType)]
        public decimal Purcase { get; set; }

        [Required]
        [Column(TypeName = DecimalColumnType)]
        public decimal LastDiv { get; set; }

        [Required]
        public long MarketCap { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
