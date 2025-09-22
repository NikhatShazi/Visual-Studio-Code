using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GameStore.Api.Data;

public class GameStoreContextFactory : IDesignTimeDbContextFactory<GameStoreContext>
    {
        public GameStoreContext CreateDbContext(string[] args)
        {
            // Load configuration (so migrations work outside of runtime)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GameStoreContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("GameStore"));

            return new GameStoreContext(optionsBuilder.Options);
        }
    }
