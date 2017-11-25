
using CarInventory.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace CarInventory.Data
{
    public class BigBangInitializer : DropCreateDatabaseIfModelChanges<CarInventoryDbContext>
    {
        protected override void Seed(CarInventoryDbContext context)
        {
            Initialize(context);
            base.Seed(context);
        }

        public void Initialize(CarInventoryDbContext context)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }

                if (!roleManager.RoleExists("Member"))
                {
                    roleManager.Create(new IdentityRole("Member"));
                }

                var user = new ApplicationUser()
                {
                    Email = "karthikeyanyuvaraj@yahoo.com",
                    UserName = "test@test.com",
                    FirstName = "Karthikeyan",
                    LastName = "Yuvaraj"
                };

                var userResult = userManager.Create(user, "Admin@123");

                if (userResult.Succeeded)
                {
                    userManager.AddToRole<ApplicationUser, string>(user.Id, "Admin");
                }

                context.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
