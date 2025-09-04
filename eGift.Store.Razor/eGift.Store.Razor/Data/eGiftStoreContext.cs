using eGift.Store.Razor.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eGift.Store.Razor.Data
{
    public class eGiftStoreContext : DbContext
    {
        #region Constructors
        public eGiftStoreContext(DbContextOptions<eGiftStoreContext> options)
            : base(options)
        {
        }
        #endregion

        #region Datasets
        public DbSet<eGift.Store.Razor.Models.ViewModels.CustomerViewModel> CustomerViewModel { get; set; }
        public DbSet<eGift.Store.Razor.Models.ViewModels.SigninViewModel> SigninViewModel { get; set; }
        #endregion

        #region On Model Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
        .Entity<SigninViewModel>(
            eb =>
            {
                eb.HasNoKey();
            });
        }
        #endregion
    }
}
