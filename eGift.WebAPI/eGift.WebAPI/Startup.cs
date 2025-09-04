using eGift.WebAPI.Common;
using eGift.WebAPI.Models.DataModels;
using eGift.WebAPI.Models.DBContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGift.WebAPI
{
    public class Startup
    {
        #region Constructors
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Use SQLite instead of SQL Server
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                             IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APILogin v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            CreateDefaultUser(serviceProvider);
        }

        //To create default user.
        private void CreateDefaultUser(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDBContext>>()))
            {
                var defaultUser = context.EmployeeModel.Where(x => !x.IsDeleted && x.IsDefault && x.Mobile == "8788530827").FirstOrDefault();
                if (defaultUser == null)
                {
                    var superAdmin = new EmployeeModel()
                    {
                        FirstName = "Nikhat",
                        LastName = "Ansari",
                        DateOfBirth = Convert.ToDateTime("1990-11-18"),
                        GenderId = (int)Gender.Female,
                        Mobile = "8788530827",
                        Email = "nikhatshazi18@gmail.com",
                        IsActive = true,
                        RoleId = (int)RoleType.SuperAdmin,
                        IsDefault = true,
                        IsDeleted = false,
                        CreatedBy = 0,
                        CreatedDate = DateTime.Now
                    };
                    context.EmployeeModel.Add(superAdmin);
                    context.SaveChanges();

                    if (superAdmin.ID > 0)
                    {
                        var superAdminLogin = new LoginModel()
                        {
                            RefId = superAdmin.ID,
                            RefType = RoleType.SuperAdmin.ToString(),
                            UserName = "Nikhat",
                            Password = "Test@1234",
                            RoleId = (int)RoleType.SuperAdmin,
                            IsActive = true
                        };
                        context.LoginModel.Add(superAdminLogin);
                        context.SaveChanges();

                    }
                }
            }
        }
    }
}
