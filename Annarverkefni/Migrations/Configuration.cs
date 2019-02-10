namespace Annarverkefni.Migrations
{
    using Annarverkefni.Models;
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Annarverkefni.Models.Entity;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Annarverkefni.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(Annarverkefni.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            CreateRoles(context);
            CreateAdmin(context);
        }
        private void CreateRoles(ApplicationDbContext context)
        {
            var rolestore = new RoleStore<IdentityRole>(context);
            var rolemanager = new RoleManager<IdentityRole>(rolestore);

            if(!context.Roles.Any(x => x.Name == "admin"))
            {
                var role = new IdentityRole { Name = "admin" };
                rolemanager.Create(role);
            }
        }

        private void CreateAdmin(ApplicationDbContext context)
        {
            var userstore = new UserStore<ApplicationUser>(context);
            var usermanager = new UserManager<ApplicationUser>(userstore);

            if(!context.Users.Any(x => x.Email == "olga@fsu.is"))
            {
                var user = new ApplicationUser { Email = "olga@fsu.is", UserName = "olga@fsu.is" };
                usermanager.Create(user, "Password1!");
                usermanager.AddToRole(user.Id, "admin");
            }
        }
    }
}
