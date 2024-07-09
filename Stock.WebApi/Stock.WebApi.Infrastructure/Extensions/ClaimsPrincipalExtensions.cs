namespace Stock.WebApi.Infrastructure.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            string? id = user.FindFirstValue(ClaimTypes.NameIdentifier);

            return id;
        }
    }
}
