using Microsoft.AspNetCore.Identity;

namespace Apps.MVCApp.Models
{
    public class AppRole : IdentityRole
    {
        public string description { get; set; }
    }
}
