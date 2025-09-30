using Microsoft.EntityFrameworkCore;
namespace GameStore.Api.Data;

// This is an Extension method for WebApplication class.
// So, we can call this method in Progrm.cs on WebApplication instance,as app.MigrateDb();
//To recover the database if deleeted or not exist.
public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
