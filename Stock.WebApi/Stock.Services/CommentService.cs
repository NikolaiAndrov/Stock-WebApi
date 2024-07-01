namespace Stock.Services
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Stock.Data.Common.Repository;
    using Stock.Data.Models;
    using Stock.WebApi.DtoModels.Comment;
    using Stock.WebApi.Infrastructure.Extensions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
    }
}
