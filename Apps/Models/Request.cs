using System;

namespace Apps.MVCApp.Models
{
	public class Request
	{
		public int id { get; set; }	
		public DateTimeOffset date { get; set; }
		public string userid { get; set; }
		public  AppUser user { get; set; }
		public int clientid { get; set; }		
		public  Client client { get; set; }
	}
}
