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
                            Phone = 0643259023,
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Place = "โรงพระเทพ"
                         },
                        new MainPose()
                        {
                            FirstName = "auty",
                            LastName = "auth",
                            Section = 18,
                            Phone = 0643259024,
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Place = "โรงพระเทพ2"
                        }
                    });
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
                            Phone = 0643259023,
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
    }
}
