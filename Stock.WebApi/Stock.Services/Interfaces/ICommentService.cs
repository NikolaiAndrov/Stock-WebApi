﻿namespace Stock.Services.Interfaces
{
    using WebApi.DtoModels.Comment;

    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();

        Task<CommentDto?> GetByIdAsync(int id);

        Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto, int stockId);
    }
}
