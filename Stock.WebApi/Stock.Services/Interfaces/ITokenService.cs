namespace Stock.Services.Interfaces
{
    using Data.Models;

    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
