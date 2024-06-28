namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Stock.Data;
    using Stock.Data.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockDbContext dbContext;

        public StockController(StockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ICollection<Stock> stocks = await this.dbContext.Stocks.ToListAsync();

            return this.Ok(stocks);
        }
    }
}
