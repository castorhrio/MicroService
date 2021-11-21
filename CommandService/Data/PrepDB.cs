using System;
using System.Collections.Generic;
using CommandService.Models;
using CommandService.SyncDataService.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommandService.Data
{   
    public static class PrepDB
    {
        public static void Prepopulation(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platform = grpcClient.ReturnAllPlatforms();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(),platform);
            }
        }

        private static void SeedData(ICommandRepo repo,IEnumerable<Platform> platforms)
        {
            Console.WriteLine("------ Seeding new Platforms...");

            foreach(var item in platforms)
            {
                if(!repo.ExternalPlatformExist(item.ExtendID))
                {
                    repo.CreatePlatform(item);
                }

                repo.SaveChange();
            }
        }
    }
}