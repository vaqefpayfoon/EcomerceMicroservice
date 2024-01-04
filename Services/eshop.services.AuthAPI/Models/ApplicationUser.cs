using Microsoft.AspNetCore.Identity;

namespace eshop.services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
