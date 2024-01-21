using Microsoft.AspNetCore.Identity;

namespace healthguard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
