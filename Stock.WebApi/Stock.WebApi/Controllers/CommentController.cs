namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Stock.Services.Interfaces;
    using DtoModels.Comment;
    using static Common.ApplicationMessages;
    using Stock.Data.Models;
    using Stock.WebApi.Infrastructure.Extensions;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IStockService stockService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentController(ICommentService commentService,
            IStockService stockService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentService = commentService;
            this.stockService = stockService;
            this.userManager = userManager;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CommentDto> comments;

            try
            {
                comments = await this.commentService.GetAllAsync();
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            CommentDto? commentDto;

            try
            {
                commentDto = await this.commentService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            if (commentDto == null)
            {
                return this.NotFound();
            }

            return this.Ok(commentDto);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (await this.stockService.IsStockExistingByIdAsync(stockId) == false)
            {
                return this.BadRequest(StockNotExistingMessage);
            }

            CommentDto commentDto;

            try
            {
                string username = this.User.GetUsername()!;
                ApplicationUser? user = await this.userManager.FindByNameAsync(username);
                commentDto = await this.commentService.CreateCommentAsync(createCommentDto, stockId, user!.Id);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.CreatedAtAction(nameof(this.GetById), new { id = commentDto.Id }, commentDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentDto updateCommentDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            string username = this.User.GetUsername()!;
            ApplicationUser? user = await this.userManager.FindByNameAsync(username);

            if (await this.commentService.IsUserCreatorOfCommentAsync(id, user!.Id) == false)
            {
                return this.Unauthorized();
            }

            CommentDto? commentDto;

            try
            {
                commentDto = await this.commentService.UpdateCommentAsync(id, updateCommentDto);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            if (commentDto == null)
            {
                return this.NotFound();
            }

            return this.Ok(commentDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await this.commentService.IsCommentExistingByIdAsync(id) == false)
            {
                return this.NotFound();
            }

            string username = this.User.GetUsername()!;
            ApplicationUser? user = await this.userManager.FindByNameAsync(username);

            if (await this.commentService.IsUserCreatorOfCommentAsync(id, user!.Id) == false)
            {
                return this.Unauthorized();
            }

            try
            {
                await this.commentService.DeleteAsync(id);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }

            return this.NoContent();
        }
    }
}
