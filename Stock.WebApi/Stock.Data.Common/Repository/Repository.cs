namespace Stock.Data.Common.Repository
{
    using Microsoft.EntityFrameworkCore;

    public class Repository : IRepository
    {
        private readonly StockDbContext dbContext;

        public Repository(StockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> DbSet<T>() where T : class
            => this.dbContext.Set<T>();

        public IQueryable<T> All<T>() where T : class
            => this.DbSet<T>();

        public IQueryable<T> AllReadonly<T>() where T : class
            => this.DbSet<T>().AsNoTracking();
    }
}
