namespace Stock.Services.Interfaces
{
    using Stock.WebApi.DtoModels.Comment;

    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();
    }
}
