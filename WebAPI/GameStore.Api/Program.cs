using System.Reflection.Metadata;
using System.Security.Cryptography;
using GameStore.Api.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
new (1,
    "Street Fighter",
    "Fighter",
    199.99M,
    new DateOnly(1992, 07, 15)
    ),

new (2,
    "Final Fantacy",
    "Role Playing",
    200.00M,
    new DateOnly(2010, 08, 18)
    ),
new (
    3,
    "FiFa 23",
    "Sports",
    150.00M,
    new DateOnly(2020, 05, 12)
    )
];

//GET /games
app.MapGet("games", () => games);

//GET /games/{id}
app.MapGet("games/{id}", (int id) => games.Find(g => g.Id == id)).WithName(GetGameEndpointName);

//POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
        );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});


//PUT /games/{id}
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(g => g.Id == id);
    if (index == -1)
    {
        return Results.NotFound();
    }
    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
        );

    return Results.NoContent();
});

//DELETE /games/{id}
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(g => g.Id == id);
    return Results.NoContent();
});

app.MapGet("/", () => "Hello World!");

app.Run();
