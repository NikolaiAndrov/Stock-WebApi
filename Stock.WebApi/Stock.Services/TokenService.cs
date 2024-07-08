namespace Stock.Services
{
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;  
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Data.Models;
    using Services.Interfaces;

    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey securityKey;

        public TokenService(IConfiguration configuration, SymmetricSecurityKey securityKey)
        {
            this.configuration = configuration;
            this.securityKey = securityKey;
        }

        public string CreateToken(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!)
            };

            var credentials = new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = this.configuration["JWT:Issuer"],
                Audience = this.configuration["JWT:Audience"]
            };

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

            var token = securityTokenHandler.CreateToken(tokenDescriptor);

            return securityTokenHandler.WriteToken(token);
        }
    }
}
