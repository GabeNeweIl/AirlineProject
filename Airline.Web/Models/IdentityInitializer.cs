using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Airline.Web.Models
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var admin = new IdentityRole { Name = "admin" };
            var dispatcher = new IdentityRole { Name = "dispatcher" };
            var user = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(admin);
            roleManager.Create(dispatcher);
            roleManager.Create(user);
            // создаем пользователей
            var adminUser = new ApplicationUser { Email = "admin@mailforspam.com", UserName = "admin@mailforspam.com" , EmailConfirmed = true};
            var dispatcherUser = new ApplicationUser { Email = "dispatcher@mailforspam.com", UserName = "dispatcher@mailforspam.com", EmailConfirmed = true };
            var userUser = new ApplicationUser { Email = "user1@mailforspam.com", UserName = "user1@mailforspam.com", EmailConfirmed = true };
            string password = "12Qwaszx";
            var result = userManager.Create(adminUser, password);
            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(adminUser.Id, admin.Name);
            }
            result = userManager.Create(dispatcherUser, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(dispatcherUser.Id, dispatcher.Name);
            }
            result = userManager.Create(userUser, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(userUser.Id, user.Name);
            }
            base.Seed(context);
        }
    }
}