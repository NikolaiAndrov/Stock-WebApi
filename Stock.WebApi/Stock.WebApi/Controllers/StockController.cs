namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Stock.Data;
    using Stock.Data.Models;
    using static Infrastructure.Extensions.StockMappers;

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
            var stocks = await this.dbContext.Stocks
                .Select(s => s.ToStockDto())
                .ToListAsync();

            return this.Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await this.dbContext.Stocks
                .Where(s => s.Id == id)
                .Select(s => s.ToStockDto())
                .FirstOrDefaultAsync();

            if (stock == null)
            {
                return this.NotFound();
            }

            return this.Ok(stock);
        }
    }
}
