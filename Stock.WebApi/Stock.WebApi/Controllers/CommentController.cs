namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stock.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
    }
}
