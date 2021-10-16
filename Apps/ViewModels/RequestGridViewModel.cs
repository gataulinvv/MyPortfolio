using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.ViewModels
{
	public class RequestGridViewModel
	{
		//public RequestGridViewModel() {

		//	RoleNamesList = new List<string>();

		//}

		public int Id { get; set; }

		public DateTimeOffset Date { get; set; }

		public List<string> RoleNamesList { get; set; }

		public string UserName { get; set; }
	
		public string UserEmail { get; set; }

		public string ClientName { get; set; }

		
	}
}
