namespace Stock.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Stock.Data.Models;

    public class StockDbContext : IdentityDbContext<ApplicationUser>
    {
        public StockDbContext(DbContextOptions options)
            : base(options) 
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
