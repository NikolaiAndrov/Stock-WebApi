namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stock.Services.Interfaces;
    using Stock.WebApi.DtoModels.Comment;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CommentDto> comments;

            try
            {
                comments = await this.commentService.GetAllAsync();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }

            return Ok(comments);
        }
    }
}
