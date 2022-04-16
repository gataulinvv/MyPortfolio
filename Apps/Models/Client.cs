
using System.Collections.Generic;

namespace Apps.MVCApp.Models
{
	public class Client
	{
		public Client()
		{
			requests = new HashSet<Request>();			
		}
		public int id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public  ICollection<Request> requests { get; set; }
	}
}
