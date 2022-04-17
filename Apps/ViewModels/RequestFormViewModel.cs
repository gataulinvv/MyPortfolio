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
        public string userid { get; set; }
        public IEnumerable<AppUser> userslist { get; set; }
        public int clientid { get; set; }
        public IEnumerable<Client> clientslist { get; set; }
    }
}
