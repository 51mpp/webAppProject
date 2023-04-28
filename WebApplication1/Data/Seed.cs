using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using System.Diagnostics;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.MainPoses.Any())
                {
                    context.MainPoses.AddRange(new List<MainPose>()
                    {
                        new MainPose()
                        {
                            FirstName = "Thanakorn",
                            LastName = "Rattanapornchai",
                            Section = 18,
                            Phone = "0643259023",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Place = "โรงพระเทพ",
                            MaxComment = 0
                         },
                        new MainPose()
                        {
                            FirstName = "auty",
                            LastName = "auth",
                            Section = 18,
                            Phone = "0643259023",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Place = "โรงพระเทพ2",
                            MaxComment = 0
                        }
                    }); ;
                    context.SaveChanges();
                }
                if (!context.AppUsers.Any())
                {
                    context.AppUsers.AddRange(new List<AppUser>()
                    {
                        new AppUser()
                        {
                        
                            FirstName = "Thanakorn",
                            LastName = "Rattanapornchai",
                            Section = 18,
                            Phone = "0643259023",
                            Icon = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        }
                    });
                    context.SaveChanges();
                }
                // Add comment to MainPose with Id equal to 1

                var mainPose = context.MainPoses.FirstOrDefault(m => m.Id == 1);

                if (mainPose != null)
                {
                    mainPose.Comments = new List<Comment>()
                    {
                        new Comment() { CommentText = "First1 comment" },
                        new Comment() { CommentText = "Second1 comment" }
                    };
                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Nakorn",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@123456");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "64010342@kmitl.ac.th";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
    
}
