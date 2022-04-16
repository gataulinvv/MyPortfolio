using MassTransit;
using Microsoft.AspNetCore.Identity;
using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Application.Hendlers.Users
{
    public class GetUsersListConsumer : IConsumer<GetUsersListCommand>
    {
        UserManager<AppUser> _userManager;
        public GetUsersListConsumer(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task Consume(ConsumeContext<GetUsersListCommand> context)
        {
            var usersList = _userManager.Users.Select(i => new UserGridViewModel
            {
                Id = i.Id,
                Email = i.Email,
                UserName = i.UserName

            }).OrderBy(i => i.Id).ToList();

            foreach (var u in usersList)
            {
                var user = await _userManager.FindByNameAsync(u.UserName);
                List<string> roles = (await _userManager.GetRolesAsync(user)).ToList();
                u.RoleNamesList = roles;
            }

            await context.RespondAsync(new GetUsersListResult { UsersList = usersList });
        }
    }
    public class GetUsersListCommand
    {
    }
    public class GetUsersListResult
    {
        public IEnumerable<UserGridViewModel> UsersList { get; set; }
    }
}
