using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PreDb
    {
        public static void PrePopulation(IApplicationBuilder app, bool prod)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),prod);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("------- attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"------- could not run migrations:{ex.Message}");
                }

            }
            
            if(!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data ...");

                context.Platforms.AddRange(
                    new Platform(){Name = "DotNet",Publisher="Mir",Cost="Free"},
                    new Platform(){Name = "SQL",Publisher="Mir",Cost="Free"},
                    new Platform(){Name = "Nodejs",Publisher="Mir",Cost="Free"}
                );

                context.SaveChanges();
                Console.WriteLine("Seeding Data Done");
                return;
            }

            Console.WriteLine("db already have data");
        }
    }
}