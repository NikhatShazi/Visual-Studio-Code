using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClients
{
    private readonly List<GameSummary> games = [

        new GameSummary { Id = 1,
                        Name = "StreetFighterII",
                        Genre = "Fighting",
                        Price = 59.99m,
                        ReleaseDate = new DateOnly(2017, 3, 3)
                        },
        new GameSummary { Id = 2,
                          Name = "Mario",
                          Genre = "Action", Price = 49.99m,
                          ReleaseDate = new DateOnly(2018, 4, 20)
                        },
        new GameSummary { Id = 3,
                          Name = "Dead Redemption",
                          Genre = "Adventure",
                          Price = 39.99m,
                          ReleaseDate = new DateOnly(2018, 10, 26)
                        },
    ];
    
    public GameSummary[] GetGames()=>[.. games];
}

