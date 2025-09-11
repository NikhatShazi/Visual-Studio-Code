using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DailyTaskBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Load connection string
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DailyTaskContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DailyTaskBotConnection"));

            using (var context = new DailyTaskContext(optionsBuilder.Options))
            {
                // Optional: create database if it doesn't exist
                context.Database.Migrate();
            }

            Application.Run(new DailyTasks(optionsBuilder.Options));
        }
    }
}
