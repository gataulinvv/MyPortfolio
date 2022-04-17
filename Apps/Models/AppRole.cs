using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Models
{
    public class AppRole : IdentityRole
    {
        public string description { get; set; }
    }
}
