using MassTransit;
using Microsoft.AspNetCore.Identity;
using Apps.MVCApp.Models;
using System.Threading.Tasks;

namespace Apps.MVCApp.Application.Hendlers.Users
{
	public class DeleteUserConsumer : IConsumer<DeleteUserCommand>
	{
		UserManager<AppUser> _userManager;
		public DeleteUserConsumer(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task Consume(ConsumeContext<DeleteUserCommand> context)
		{
			var user = await _userManager.FindByIdAsync(context.Message.UserId);
			if (user == null)
				await context.RespondAsync(new DeleteUserResult { Text = "Item is not found!" });
			else
			{
				var result = await _userManager.DeleteAsync(user);

				if (result.Succeeded)
					await context.RespondAsync(new DeleteUserResult { Succeeded = true, Text = "Item is deleted!" });
				else
					await context.RespondAsync(new DeleteUserResult { Succeeded = false, Text = "Item is not not deleted!" });
			}
		}
	}
	public class DeleteUserCommand
	{
		public string UserId { get; set; }
	}
	public class DeleteUserResult
	{
		public bool Succeeded { get; set; }
		public string Text { get; set; }
	}
}



