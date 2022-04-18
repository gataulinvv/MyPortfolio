using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apps.MVCApp.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Apps.MVCApp.Application.Hendlers.Users
{
    public class UpdateUserConsumer : IConsumer<UpdateUserCommand>
    {
        UserManager<AppUser> _userManager;

        RoleManager<IdentityRole> _roleManager;

        public UpdateUserConsumer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Consume(ConsumeContext<UpdateUserCommand> context)
        {
            var model = context.Message.User;

            var httpContext = context.Message.HttpContext;

            AppUser user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                user.Email = model.Email;

                user.UserName = model.UserName;

                if (model.password != "" && model.password != null)
                {
                    var _passwordValidator = httpContext.RequestServices.GetService(typeof(IPasswordValidator<AppUser>)) as IPasswordValidator<AppUser>;

                    var _passwordHasher = httpContext.RequestServices.GetService(typeof(IPasswordHasher<AppUser>)) as IPasswordHasher<AppUser>;

                    IdentityResult isCorrect = await _passwordValidator.ValidateAsync(_userManager, user, model.password);

                    if (isCorrect.Succeeded)
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.password);
                    else
                        await context.RespondAsync(new UpdateUserResult { Succeeded = false, Text = "Password is not correct!" });

                }

                List<string> domainRoles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

                //Подписать пользователя на роли
                var intersect = domainRoles.Intersect(model.userroles);

                foreach (var roleName in intersect)
                    await _userManager.AddToRoleAsync(user, roleName);

                //Отписать пользователя от ролей
                var except = domainRoles.Except(model.userroles);

                foreach (var roleName in except)
                    await _userManager.RemoveFromRoleAsync(user, roleName);

                if ((await _userManager.UpdateAsync(user)).Succeeded == true)
                    await context.RespondAsync(new UpdateUserResult { Succeeded = true, Text = "Item is updated!" });
                else
                    await context.RespondAsync(new UpdateUserResult { Succeeded = false, Text = "Item is not updated!" });
            }
            else
                await context.RespondAsync(new UpdateUserResult { Succeeded = false, Text = "Item is not found!" });
        }
    }
    public class UpdateUserCommand
    {
        public HttpContext HttpContext { get; set; }
        public AppUser User { get; set; }
    }
    public class UpdateUserResult
    {
        public bool Succeeded { get; set; }
        public string Text { get; set; }
    }
}
