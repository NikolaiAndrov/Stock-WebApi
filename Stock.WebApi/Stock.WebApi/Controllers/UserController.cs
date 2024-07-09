namespace Stock.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Stock.Data.Models;
    using Stock.Services.Interfaces;
    using Stock.WebApi.DtoModels.User;
    using static Common.ApplicationMessages;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;

        public UserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
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

                NewUserDto newUser = new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = this.tokenService.CreateToken(user)
                };

                return this.Ok(newUser);
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ApplicationUser? user = await this.userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
            {
                return this.Unauthorized(InvalidUsernameOrPasswordMessage);
            }

            var result = await this.signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return this.Unauthorized(InvalidUsernameOrPasswordMessage);
            }

            NewUserDto userDto;

            try
            {
                userDto = new NewUserDto
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Token = this.tokenService.CreateToken(user)
                };
            }
            catch (Exception)
            {
                return this.BadRequest(UnexpectedErrorMessage);
            }
            
            return this.Ok(userDto);
        }
    }
}
