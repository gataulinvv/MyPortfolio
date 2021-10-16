using System;

namespace Apps.MVCApp.Models
{
	public class Request
	{
		public int id { get; set; }	
		public DateTimeOffset date { get; set; }

		public string user_id { get; set; }

		public virtual AppUser user { get; set; }

		public int client_id { get; set; }

		public virtual Client client { get; set; }

	}
}
