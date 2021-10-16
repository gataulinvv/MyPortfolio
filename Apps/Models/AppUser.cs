using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Apps.MVCApp.Models
{
	public class AppUser : IdentityUser
	{
		public AppUser() {

			requests = new HashSet<Request>();		
		}
		[NotMapped]
		public List<string> user_roles { get; set; }

		[NotMapped]
		public List<string> all_roles { get; set; }
		
		public string password { get; set; }
		public virtual ICollection<Request> requests { get; set; }

	}
}
