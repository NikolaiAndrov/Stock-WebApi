namespace Stock.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Stock
    {
        public Stock()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purcase { get; set; }

        [Required]
        public decimal LastDiv { get; set; }

        [Required]
        public string Industry { get; set; } = string.Empty;

        [Required]
        public decimal MarketCap { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
