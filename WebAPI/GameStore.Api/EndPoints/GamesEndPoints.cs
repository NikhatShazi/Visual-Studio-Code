using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
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

    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        //GET /games
        group.MapGet("/", () => games);

        //GET /games/{id}
        group.MapGet("/{id}", (int id) => games.Find(g => g.Id == id)).WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
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
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
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
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}