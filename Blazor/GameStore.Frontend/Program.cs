using GameStore.Frontend.Clients;
using GameStore.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

var gameStoreAPiUrl = builder.Configuration["GameStoreApiUrl"] ??
throw new Exception("GameStoreApiUrl is not configured.");

//Register HttpClient for GamesClients and GenresClient. To retrieve data from the API.
builder.Services.AddHttpClient<GamesClients>(
    client => client.BaseAddress = new Uri(gameStoreAPiUrl));

builder.Services.AddHttpClient<GenresClient>(
    client => client.BaseAddress = new Uri(gameStoreAPiUrl));
//We added above lines so no need folloowin two lines.
//builder.Services.AddSingleton<GamesClients>();
//builder.Services.AddSingleton<GenresClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForErrors: true);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
