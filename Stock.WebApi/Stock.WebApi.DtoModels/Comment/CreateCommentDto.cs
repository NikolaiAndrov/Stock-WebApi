namespace Stock.WebApi.DtoModels.Comment
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.CommentValidation;

    public class CreateCommentDto
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = string.Empty;
    }
}
