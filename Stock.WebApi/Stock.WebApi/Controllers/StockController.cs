namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stock.Services.Interfaces;
    using Stock.WebApi.DtoModels.Stock;

    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService stockService;

        public StockController(IStockService stockService)
        {
            this.stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<StockDto> stocks;

            try
            {
                stocks = await this.stockService.GetAllStocksAsync();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }

            return this.Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            StockDto? stockDto = null;

            try
            {
                stockDto = await this.stockService.GetStockByIdAsync(id);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }

            if (stockDto == null)
            {
                return this.NotFound();
            }

            return this.Ok(stockDto);
        }
    }
}
