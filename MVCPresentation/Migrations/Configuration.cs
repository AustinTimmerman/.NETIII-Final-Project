namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataObjects;
    using LogicLayer;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentation.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVCPresentation.Models.ApplicationDbContext";
        }

        protected override void Seed(MVCPresentation.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@company.com";
            const string adminPassword = "$Password1";

            LogicLayer.UserManager userMgr = new LogicLayer.UserManager();
            var roles = userMgr.RetrieveUserRoles();
            foreach(var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
            }
            if (!roles.Contains("Admin"))
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" });
            }

            if(!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "Company",
                    UserID = 1
                };

                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    context.SaveChanges();

                    var usrMgr = new UserManager();
                    try
                    {
                        
                        usrMgr.InsertNewUser(admin, admin, usrMgr.HashSha256(adminPassword), roles);
                        // Refresh the newly-added user to get the user ID.
                        int newUser = usrMgr.RetrieveUserIDFromEmail(admin);
                        //usrMgr.AddUserRole(newUser, "Administrator");
                    }
                    catch (Exception ex)
                    {
                        // We want it to be brought to the user's attention that this didn't work
                        throw;
                    }

                }
            }

            
        }
    }
}
