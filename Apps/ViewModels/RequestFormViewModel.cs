using Apps.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.ViewModels
{
	public class RequestFormViewModel
	{
		public int Id { get; set; }
		public DateTimeOffset date { get; set; }


		public string user_id { get; set; }

	
		public IEnumerable<AppUser> users_list { get; set; }


		public int client_id { get; set; }

		public IEnumerable<Client> clients_list { get; set; }

	}
}
