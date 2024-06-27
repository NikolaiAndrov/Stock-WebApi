namespace Stock.Data
{
    using Microsoft.EntityFrameworkCore;
    
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions options)
            : base(options) 
        {
            
        }
    }
}
