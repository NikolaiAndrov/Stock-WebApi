namespace Stock.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.CommentValidation;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int StockId { get; set; }

        [ForeignKey(nameof(StockId))]
        public virtual Stock Stock { get; set; } = null!;
    }
}
