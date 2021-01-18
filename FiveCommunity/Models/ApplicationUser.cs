using Microsoft.AspNetCore.Identity;

namespace FiveCommunity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
    }
}
