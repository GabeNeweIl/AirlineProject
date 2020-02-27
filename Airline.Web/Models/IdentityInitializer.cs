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
            var root = new IdentityRole { Name = "root" };
            var dispatcher = new IdentityRole { Name = "dispatcher" };
            var user = new IdentityRole { Name = "user" };


            // добавляем роли в бд
            roleManager.Create(root);
            roleManager.Create(admin);
            roleManager.Create(dispatcher);
            roleManager.Create(user);
            // создаем пользователей
            var rootUser = new ApplicationUser { Email = "smeni0nik@gmail.com", UserName = "smeni0nik@gmail.com" , EmailConfirmed = true};
            string password = "12Qwaszx";
            var result = userManager.Create(rootUser, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(rootUser.Id, root.Name);
            }

            base.Seed(context);
        }
    }
}