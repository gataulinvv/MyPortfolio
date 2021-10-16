using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Apps.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Application.Hendlers.Users
{
	public class CreateUserConsumer : IConsumer<CreateUserCommand>
	{
		UserManager<AppUser> _userManager;

		RoleManager<IdentityRole> _roleManager;

		public CreateUserConsumer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;

			_roleManager = roleManager;
		}
		public async Task Consume(ConsumeContext<CreateUserCommand> context)
		{
			var model = context.Message.User;

			var httpContext = context.Message.HttpContext;

			
				var user = new AppUser { UserName = model.UserName, Email = model.Email, user_roles = model.user_roles };

				//Проверить пароль на валидность			
				var _passwordValidator = httpContext.RequestServices.GetService(typeof(IPasswordValidator<AppUser>)) as IPasswordValidator<AppUser>;
				var _passwordHasher = httpContext.RequestServices.GetService(typeof(IPasswordHasher<AppUser>)) as IPasswordHasher<AppUser>;
				IdentityResult isCorrect = await _passwordValidator.ValidateAsync(_userManager, user, model.password);

				if (isCorrect.Succeeded)
				{
					user.PasswordHash = _passwordHasher.HashPassword(user, model.password);

					if ((await _userManager.CreateAsync(user, model.password)).Succeeded == true)
					{
						//Подписать пользователя на роли
						List<string> domainRoles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

						var intersect = domainRoles.Intersect(model.user_roles);

						foreach (var roleName in intersect)
							await _userManager.AddToRoleAsync(user, roleName);

						await context.RespondAsync(new CreateUserResult {Succeeded = true, User = user, Text = "Item is created!" });;;

					}
					else
						await context.RespondAsync(new CreateUserResult { Succeeded = false, Text = "Item is not created!" });
				}
				else
					await context.RespondAsync(new CreateUserResult { Succeeded = false, Text = "Password is not correct!" });
			}
			
		}
	public class CreateUserCommand
	{
		public HttpContext HttpContext { get; set; }
		public AppUser User { get; set; }

	}


	public class CreateUserResult
	{
		public bool Succeeded { get; set; }
		public AppUser User { get; set; }
		public string Text { get; set; }
	}
}




