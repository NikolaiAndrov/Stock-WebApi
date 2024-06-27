namespace Stock.Data
{
    using Microsoft.EntityFrameworkCore;
    using Stock.Data.Models;

    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions options)
            : base(options) 
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
