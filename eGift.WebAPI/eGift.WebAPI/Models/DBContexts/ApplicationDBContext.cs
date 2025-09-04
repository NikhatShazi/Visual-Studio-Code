using eGift.WebAPI.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace eGift.WebAPI.Models.DBContexts
{
    public class ApplicationDBContext : DbContext
    {
        #region Contructors
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        #endregion

        #region Datasets
        public DbSet<AddressModel> AddressModel { get; set; }
        public DbSet<CategoryModel> CategoryModel { get; set; }
        public DbSet<CityModel> CityModel { get; set; }
        public DbSet<CountryModel> CountryModel { get; set; }
        public DbSet<CustomerModel> CustomerModel { get; set; }
        public DbSet<EmployeeModel> EmployeeModel { get; set; }
        public DbSet<GenderModel> GenderModel { get; set; }
        public DbSet<LoginModel> LoginModel { get; set; }
        public DbSet<OrderDetailsModel> OrderDetailsModel { get; set; }
        public DbSet<OrderModel> OrderModel { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
        public DbSet<RoleModel> RoleModel { get; set; }
        public DbSet<StateModel> StateModel { get; set; }
        public DbSet<SubCategoryModel> SubCategoryModel { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var id = entityType.FindProperty("ID");
                if (id != null)
                {
                    id.SetColumnType("INTEGER"); // SQLite requires INTEGER
                }
            }            
        }
    }
}
