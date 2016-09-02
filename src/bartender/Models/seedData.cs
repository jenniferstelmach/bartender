using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bartender.Data;


namespace bartender.Models
{
    public class seedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // look for any drinks already in database
                if (context.drinkList.Any())
                {
                    return;     //DB already seeded
                }

                context.drinkList.AddRange(
                    new drinkList
                    {
                        Category = "Beer",
                        Drink = "Blue Moon"
                    },
                new drinkList
                {
                    Category = "Beer",
                    Drink = "Michelob Ultra"
                },
                new drinkList
                {
                    Category = "Wine",
                    Drink = "Pinot Grigio"
                },
                new drinkList
                {
                    Category = "Mixed Drink",
                    Drink = "Sex on the Beach"
                },
                new drinkList
                {
                    Category = "Mixed Drink",
                    Drink = "Vodka and cranberry"
                }
              );

                context.SaveChanges();

            }

        }
    }
}
