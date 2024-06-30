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
            StockDto? stockDto;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            StockDto stockDto;

            try
            {
                stockDto = await this.stockService.CreateStockAsync(createStockDto);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }

            return this.CreatedAtAction(nameof(this.GetById), new { stockDto.Id }, stockDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            StockDto? stockDto;

            try
            {
                stockDto = await this.stockService.UpdateAsync(id, updateStockDto);
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await this.stockService.IsStockExistingByIdAsync(id) == false)
            {
                return this.NotFound();
            }

            try
            {
                await this.stockService.DeleteAsync(id);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }

            return this.NoContent();
        }
    }
}
