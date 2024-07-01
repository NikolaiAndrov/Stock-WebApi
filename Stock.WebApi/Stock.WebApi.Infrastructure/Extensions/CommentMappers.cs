namespace Stock.WebApi.Infrastructure.Extensions
{
    using Stock.Data.Models;
    using Stock.WebApi.DtoModels.Comment;

    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            CommentDto commentDto = new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };

            return commentDto;
        }
    }
}
