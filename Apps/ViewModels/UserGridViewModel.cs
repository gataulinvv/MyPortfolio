using System.Collections.Generic;

namespace Apps.MVCApp.ViewModels
{
    public class UserGridViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> RoleNamesList { get; set; }
    }
}
