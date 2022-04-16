using MassTransit;
using Microsoft.AspNetCore.Identity;
using Apps.MVCApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Application.Hendlers.Users
{
	public class GetUserByIdConsumer : IConsumer<GetUserByIdCommand>
	{
		UserManager<AppUser> _userManager;

		RoleManager<IdentityRole> _roleManager;

		MVCAppContext _DBcontext;
		public GetUserByIdConsumer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, MVCAppContext DBcontext)
		{

			_roleManager = roleManager;

			_userManager = userManager;

			_DBcontext = DBcontext;
		}
		public async Task Consume(ConsumeContext<GetUserByIdCommand> context)
		{
			var result = await _userManager.FindByIdAsync(context.Message.UserId);

			if (result == null)
			{
				result = new AppUser { Id = "0", UserName = "", Email = "" };
			}
			result.userroles = (await _userManager.GetRolesAsync(result)).ToList();

			result.allroles = _roleManager.Roles.Select(i => i.Name).ToList();

			await context.RespondAsync(result);
		}
	}

	public class GetUserByIdCommand
	{
		public string UserId { get; set; }
	}
	public class GetUserByIdResult
	{
		public string Text { get; set; }
	}
}

