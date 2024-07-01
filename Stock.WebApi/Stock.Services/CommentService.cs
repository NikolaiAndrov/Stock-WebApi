namespace Stock.Services
{
    using Interfaces;
    using Stock.Data.Common.Repository;

    public class CommentService : ICommentService
    {
        private readonly IRepository repository;

        public CommentService(IRepository repository)
        {
            this.repository = repository;
        }
    }
}
