namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Data.Models;
    using Services.Interfaces;
    using WebApi.DtoModels.Stock;
    using WebApi.Infrastructure.Extensions;
    using static Common.ApplicationMessages;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService portfolioService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStockService stockService;

        public PortfolioController(IPortfolioService portfolioService,
            UserManager<ApplicationUser> userManager,
            IStockService stockService)
        {
            this.portfolioService = portfolioService;
            this.userManager = userManager;
            this.stockService = stockService;

        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            string? username = this.User.GetUsername();

            if (username == null)
            {
                return this.Unauthorized();
            }

            ApplicationUser? user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            IEnumerable<StockDto> stockDtos;

            try
            {
                stockDtos = await this.portfolioService.GetUserPortfolioAsync(user.Id);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.Ok(stockDtos);
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> AddPortfolio([FromRoute] int id)
        {
            if (await this.stockService.IsStockExistingByIdAsync(id) == false)
            {
                return this.NotFound(StockNotExistingMessage);
            }

            string username = this.User.GetUsername()!;

            ApplicationUser? user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.Unauthorized(); 
            }

            if (await this.portfolioService.IsPortfolioAlreadyAddedAsync(user.Id, id))
            {
                return this.BadRequest(PortfolioAlreadyAddedMessage);
            }

            try
            {
                await this.portfolioService.CreatePortfolioAsync(user.Id, id);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.Created();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePortfolio([FromRoute] int id)
        {
            if (await this.stockService.IsStockExistingByIdAsync(id) == false)
            {
                return this.NotFound();
            }


            string username = this.User.GetUsername()!;
            ApplicationUser? applicationUser = await this.userManager.FindByNameAsync(username);

            if (await this.portfolioService.IsPortfolioAlreadyAddedAsync(applicationUser!.Id, id) == false)
            {
                return this.NotFound();
            }

            try
            {
                await this.portfolioService.DeletePortfolioAsync(applicationUser!.Id, id);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.Ok();
        }
    }
}
