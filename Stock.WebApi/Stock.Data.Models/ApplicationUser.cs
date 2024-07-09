namespace Stock.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Portfolio = new HashSet<Portfolio>();
        }

        public virtual ICollection<Portfolio> Portfolio { get; set; }
    }
}
