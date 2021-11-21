using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> opt) : base(opt)
        {

        }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //每个平台有多条指令，每条指令可以对应多个平台，所以用！
            modelBuilder
            .Entity<Platform>()
            .HasMany(p=>p.Commands)
            .WithOne(p=>p.Platform!)
            .HasForeignKey(p=>p.PlatformId);

            //每个平台有多条指令
            modelBuilder
            .Entity<Command>()
            .HasOne(p=>p.Platform)
            .WithMany(p=>p.Commands)
            .HasForeignKey(p=>p.PlatformId);
        }
    }
}