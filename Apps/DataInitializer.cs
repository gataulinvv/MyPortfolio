using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Apps.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Apps.MVCApp
{

    public class DataInitializer
    {


        public static async Task InitializeAsync(MVCAppContext DBcontext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.Roles.Any())
                await CreateRoles(roleManager);

            if (!userManager.Users.Any())
                await CreateUsers(userManager, roleManager);

            if (!DBcontext.clients.Any())
                await CreateClients(DBcontext);

            if (!DBcontext.requests.Any())
                await CreateRequests(DBcontext, userManager);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new AppRole { Name = "admin", description = "Администратор" });

            await roleManager.CreateAsync(new AppRole { Name = "manager", description = "Управляющий" });

            await roleManager.CreateAsync(new AppRole { Name = "accountant", description = "Бухгалтер" });

            await roleManager.CreateAsync(new AppRole { Name = "logist", description = "Логист" });

            //await roleManager.CreateAsync(new IdentityRole("user"));

            //await roleManager.CreateAsync(new IdentityRole("manager"));


        }

        private static async Task CreateUsers(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            //Create admin 
            var admin = new AppUser { UserName = "Admin", Email = "admin@mvcapp.com" };
            await _userManager.CreateAsync(admin, "123");

            var rolesList = _roleManager.Roles.ToList();

            for (int j = 0; j < rolesList.Count(); j++)
                await _userManager.AddToRoleAsync(admin, rolesList[j].Name);

            string[] namesList = new string[] { "Alex", "Ivan", "Tom", "Elise", "Mariya", "Bob" };

            var rnd = new Random();


            for (int i = 0; i < rolesList.Count(); i++)
            {
                var role = rolesList[i];

                var index = rnd.Next(0, namesList.Length);

                var userName = namesList[index];

                var user = new AppUser
                {
                    UserName = string.Format("{0}{1}", userName, i),
                    Email = string.Format("{0}{1}@mvcapp.com", userName, i)
                };

                await _userManager.CreateAsync(user, "123");

                await _userManager.AddToRoleAsync(user, role.Name);

            }



        }



        private static async Task CreateClients(MVCAppContext DBcontext)
        {

            await DBcontext.clients.AddAsync(new Client { name = "Yandex", email = "client@Yandex.com" });

            await DBcontext .clients.AddAsync(new Client { name = "Google", email = "client@Google.com" });

            await DBcontext.clients.AddAsync(new Client { name = "Microsoft", email = "client@Microsoft.com" });

            await DBcontext.SaveChangesAsync();
        }

        private static async Task CreateRequests(MVCAppContext _DBcontext, UserManager<AppUser> _userManager)
        {
            var users = _userManager.Users.ToList();

            var clients = _DBcontext.clients.ToList();

            var requests = new List<Request>();

            for (int i = 0; i < users.Count(); i++)
            {
                var user = users[i];

                var rnd = new Random();

                var index = rnd.Next(0, clients.Count());

                var client = clients[index];

                _DBcontext.requests.Add(new Request
                {
                    date = new DateTime(2021, 08, 31).AddMonths(-i).AddDays(i),
                    client_id = client.id,
                    client = client,
                    user_id = user.Id,
                    user = user
                });

            }
            
            await _DBcontext.SaveChangesAsync();

        }
    }
}
