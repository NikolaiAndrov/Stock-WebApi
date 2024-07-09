namespace Stock.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Portfolio
    {
        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public int StockId { get; set; }

        [ForeignKey(nameof(StockId))]
        public virtual Stock Stock { get; set; } = null!;
    }
}
