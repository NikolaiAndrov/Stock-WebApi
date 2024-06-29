namespace Stock.Data.Common.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

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

        public async Task AddAsync<T>(T entity) where T : class
            => await this.DbSet<T>().AddAsync(entity); 

        public void Delete<T>(T entity) where T : class
            => this.DbSet<T>().Remove(entity);

        public async Task<int> SaveChangesAsync()
            => await this.dbContext.SaveChangesAsync();
    }
}
