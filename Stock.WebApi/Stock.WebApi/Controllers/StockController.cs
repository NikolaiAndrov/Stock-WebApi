﻿namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using DtoModels.Stock;
    using static Common.ApplicationErrorMessages;

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
        public async Task<IActionResult> GetAll([FromQuery] StockQueryModel stockQueryModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IEnumerable<StockDto> stocks;

            try
            {
                stocks = await this.stockService.GetAllStocksAsync(stockQueryModel);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            StockDto? stockDto;

            try
            {
                stockDto = await this.stockService.GetStockByIdAsync(id);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
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
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.CreatedAtAction(nameof(this.GetById), new { id = stockDto.Id }, stockDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            StockDto? stockDto;

            try
            {
                stockDto = await this.stockService.UpdateAsync(id, updateStockDto);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            if (stockDto == null)
            {
                return this.NotFound();
            }

            return this.Ok(stockDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
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
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.NoContent();
        }
    }
}
