using Microsoft.EntityFrameworkCore;
using POCGraphQL.Models;

namespace POCGraphQL.DbContexts
{
    public class AppDbContext : DbContext
    {
        public void FillMockData()
        {
            List<Platform> platforms = new()
            {
                new() { Id = 1, Name = ".Net 8", LicenseKey = null,
                    Commands = new List<Command>
                    {
                        new()
                        {
                            CommandLine = "dotnet build",
                            HowTo = "Build a project",
                            PlatformId = 1
                        },
                        new()
                        {
                            CommandLine = "dotnet run",
                            HowTo = "dotnet run",
                            PlatformId = 1
                        }
                    }
                },
                new() { Id = 2, Name = "Docker", LicenseKey = null,
                Commands = new List<Command>
                    {
                        new()
                        {
                            CommandLine = "docker-compose up -d",
                            HowTo = "Start a docker compose file",
                            PlatformId = 2
                        },
                        new()
                        {
                            CommandLine = "docker-compose stop",
                            HowTo = "Stop a docker compose file",
                            PlatformId = 2
                        }
                    }},
                new() { Id = 3, Name = "Windows", LicenseKey = "123445678" }
            };

            AddRange(platforms);
            SaveChanges();
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Platform>().OwnsMany(x  => x.Commands));
        }
    }
}
