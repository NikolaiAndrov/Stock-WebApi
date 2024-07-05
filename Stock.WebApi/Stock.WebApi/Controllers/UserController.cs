namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Stock.Data.Models;
    using Stock.WebApi.DtoModels.User;
    using static Common.ApplicationErrorMessages;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return BadRequest(this.ModelState);
                }

                ApplicationUser user = new ApplicationUser
                {
                    UserName = registerUserDto.UserName,
                    Email = registerUserDto.Email
                };

                IdentityResult userCreated = await this.userManager.CreateAsync(user, registerUserDto.Password);

                if (!userCreated.Succeeded)
                {
                    foreach (IdentityError error in userCreated.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return this.BadRequest(this.ModelState);
                }

                IdentityResult roleResult = await this.userManager.AddToRoleAsync(user, "User");

                if (!roleResult.Succeeded)
                {
                    foreach(IdentityError error in roleResult.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return this.BadRequest(this.ModelState);
                }

                return this.Ok("User created");
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }
        }
    }
}
