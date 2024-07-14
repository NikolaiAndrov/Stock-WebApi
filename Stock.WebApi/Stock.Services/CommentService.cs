namespace Stock.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Interfaces;
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

        public async Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto, int stockId, string userId)
        {
            Comment comment = createCommentDto.ToComment(stockId, userId);

            await this.repository.AddAsync<Comment>(comment);
            await this.repository.SaveChangesAsync();

            return comment.ToCommentDto();
        }

        public async Task DeleteAsync(int id)
        {
            Comment? comment = await this.repository.GetByIdAsync<Comment>(id);
            this.repository.Delete<Comment>(comment!);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            IEnumerable<CommentDto> comments = await this.repository.AllReadonly<Comment>()
                .Include(c => c.User)
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

        public async Task<bool> IsCommentExistingByIdAsync(int id)
        {
            bool isExisting = await this.repository.AllReadonly<Comment>()
                .AnyAsync(c => c.Id == id);

            return isExisting;
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
