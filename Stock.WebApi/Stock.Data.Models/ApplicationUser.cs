namespace Stock.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Portfolio = new HashSet<Portfolio>();
            this.Comments = new HashSet<Comment>();
        }

        public virtual ICollection<Portfolio> Portfolio { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
