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
                          Genre = "Role Playing", Price = 49.99m,
                          ReleaseDate = new DateOnly(2018, 4, 20)
                        },
        new GameSummary { Id = 3,
                          Name = "Dead Redemption",
                          Genre = "Adventure",
                          Price = 39.99m,
                          ReleaseDate = new DateOnly(2018, 10, 26)
                        },
    ];

  public object? Genres { get; private set; }
  private readonly Genre[] genres = new GenresClient().GetGenres();

  public GameSummary[] GetGames() => [.. games];

  public void AddGame(GameDetails game)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);
    //var genre = genres.Single(genre => genre.Id == int.Parse(game.GenreId));
    Genre genre = GetGenreById(game.GenreId);

    var newGame = new GameSummary
    {
      Id = games.Count + 1,
      Name = game.Name,
      Genre = genre.Name,
      Price = game.Price,
      ReleaseDate = game.ReleaseDate
    };
    games.Add(newGame);
  }

  //Edit Game
  public GameDetails GetGameById(int id)
  {
    GameSummary game = GetGameSummaryById(id);
    ArgumentNullException.ThrowIfNull(game);

    var genre = genres.Single(genre => string.Equals(
                genre.Name,
                game.Genre,
                StringComparison.OrdinalIgnoreCase));

    return new GameDetails
    {
      Id = game.Id,
      Name = game.Name,
      GenreId = genre.Id.ToString(),
      Price = game.Price,
      ReleaseDate = game.ReleaseDate
    };
  }
  private Genre GetGenreById(string? id)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(id);
    return genres.Single(genre => genre.Id == int.Parse(id));
  }

  public void UpdateGame(GameDetails updateGame)
  {
    var genre = GetGenreById(updateGame.GenreId);
    GameSummary existingGame = GetGameSummaryById(updateGame.Id);

    existingGame.Name = updateGame.Name;
    existingGame.Genre = genre.Name;
    existingGame.Price = updateGame.Price;
    existingGame.ReleaseDate = updateGame.ReleaseDate;
  }

  private GameSummary GetGameSummaryById(int id)
  {
    GameSummary? game = games.Find(game => game.Id == id);
    ArgumentNullException.ThrowIfNull(game);
    return game;
  }
}

