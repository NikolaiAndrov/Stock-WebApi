namespace Stock.WebApi.Infrastructure.Extensions
{
    using Data.Models;
    using DtoModels.Comment;

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
                StockId = comment.StockId,
                CreatedBy = comment.User.UserName!
            };

            return commentDto;
        }

        public static Comment ToComment(this CreateCommentDto createCommentDto, int stockId, string userId)
        {
            Comment comment = new Comment
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = stockId,
                ApplicationUserId = userId
            };

            return comment;
        }
    }
}
