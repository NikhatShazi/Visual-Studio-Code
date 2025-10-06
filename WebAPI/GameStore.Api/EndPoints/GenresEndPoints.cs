using System;
using System.Text.RegularExpressions;
using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;
public static class GenresEndPoints
{
    public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) =>
        await dbContext.Genres
                .Select(genre => genre.ToDto())
                .AsNoTracking()
                .ToListAsync());

        return group;
    }
}