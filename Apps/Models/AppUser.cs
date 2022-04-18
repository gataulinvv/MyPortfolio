using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Apps.MVCApp.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            requests = new HashSet<Request>();
        }
        [NotMapped]
        public List<string> userroles { get; set; }

        [NotMapped]
        public List<string> allroles { get; set; }
        public string password { get; set; }
        public ICollection<Request> requests { get; set; }

    }
}
