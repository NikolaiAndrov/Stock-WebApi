namespace Stock.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Data.Common.Repository;
    using Data.Models;
    using WebApi.DtoModels.Comment;
    using WebApi.Infrastructure.Extensions;

    public class CommentService : ICommentService
    {
        private readonly IRepository repository;

        public CommentService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto, int stockId)
        {
            Comment comment = createCommentDto.ToComment(stockId);

            await this.repository.AddAsync<Comment>(comment);
            await this.repository.SaveChangesAsync();

            return comment.ToCommentDto();
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            IEnumerable<CommentDto> comments = await this.repository.AllReadonly<Comment>()
                .Select(c => c.ToCommentDto())
                .ToListAsync();

            return comments;
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            Comment? comment = await this.repository.GetByIdAsync<Comment>(id);

            if (comment == null)
            {
                return null;
            }

            return comment.ToCommentDto();
        }

        public async Task<CommentDto?> UpdateCommentAsync(int id, UpdateCommentDto updateCommentDto)
        {
            Comment? comment = await this.repository.GetByIdAsync<Comment>(id);

            if(comment == null)
            {
                return null;
            }

            comment.Title = updateCommentDto.Title;
            comment.Content = updateCommentDto.Content;

            await this.repository.SaveChangesAsync();
            return comment.ToCommentDto();
        }
    }
}
