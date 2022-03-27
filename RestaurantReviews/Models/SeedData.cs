using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReviews.Data;
using System;
using System.Linq;

namespace RestaurantReviews.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Review.Any())
                {
                    return;   // DB has been seeded
                }

                context.Review.AddRange(
                    new Review
                    {
                        Name = "Carolina Ale House",
                        Price = 0,
                        Rating = 0,
                        Text = "Dummy Text 1",
                        Bullet = "Bullet Text",
                        Address = "510 Glenwood Ave",
                        CreatedDate = DateTime.Now
                    },

                    new Review
                    {
                        Name = "Armadillo Grill",
                        Price = 0,
                        Rating = 0,
                        Text = "Dummy Text 1",
                        Bullet = "Bullet Text",
                        Address = "510 Glenwood Ave",
                        CreatedDate = DateTime.Now
                    },

                    new Review
                    {
                        Name = "McDonalds",
                        Price = 0,
                        Rating = 0,
                        Text = "Dummy Text 1",
                        Bullet = "Bullet Text",
                        Address = "510 Glenwood Ave",
                        CreatedDate = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
