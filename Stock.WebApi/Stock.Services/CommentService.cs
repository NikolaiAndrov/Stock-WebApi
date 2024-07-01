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
    }
}
