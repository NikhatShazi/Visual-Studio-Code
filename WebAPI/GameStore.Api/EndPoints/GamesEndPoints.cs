using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDto> games = [
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

    //MapGamesEndPoints is an Extension method for WebApplication class.
    //So, we can call this method in Progrm.cs on WebApplication instance,as app.MapGamesEndPoints();
    //It returns a RouteGroupBuilder, which is part of ASP.NET Core Minimal APIs.
    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        //app.MapGroup("/games"), This creates a route group with the base path /games.
        //.WithParameterValidation(),This is likely coming from the MinimalApis.Extensions library (or a similar package).
        // It enables automatic parameter validation for your endpoints.

        var group = app.MapGroup("/games")
                    .WithParameterValidation();

        //GET /games
        group.MapGet("/", async(GameStoreContext dbContext) =>
            await dbContext.Games
                .Include(games => games.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync()
        );

        //GET /games/{id}
        group.MapGet("/{id}", async(int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id); //It will find game in memory by primary key.

            return game is null ?
            Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpointName);

        //POST /games
        //Create dbContext instance, for taking advantage of Service Lifetime.
        group.MapPost("/", async(CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });


        //PUT /games/{id}
        group.MapPut("/{id}", async(int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame)
                     .CurrentValues
                     .SetValues(updatedGame.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        //DELETE /games/{id}
        group.MapDelete("/{id}", async(int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id)
                           .ExecuteDeleteAsync();
            return Results.NoContent();
        });
        return group;
    }
}