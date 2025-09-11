using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DailyTaskBot;

public class DailyTaskContextFactory : IDesignTimeDbContextFactory<DailyTaskContext>
{
    public DailyTaskContext CreateDbContext(string[] args)
    {
        // Load configuration from appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DailyTaskContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DailyTaskBotConnection"));

        return new DailyTaskContext(optionsBuilder.Options);
    }
}
