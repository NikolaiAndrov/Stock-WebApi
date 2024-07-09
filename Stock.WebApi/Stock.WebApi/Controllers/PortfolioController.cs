namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Stock.Data.Models;
    using Stock.Services.Interfaces;
    using Stock.WebApi.DtoModels.Stock;
    using Stock.WebApi.Infrastructure.Extensions;
    using System.Security.Claims;
    using static Common.ApplicationMessages;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService portfolioService;
        private readonly UserManager<ApplicationUser> userManager;

        public PortfolioController(IPortfolioService portfolioService, UserManager<ApplicationUser> userManager)
        {
            this.portfolioService = portfolioService;
            this.userManager = userManager;
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
    }
}
