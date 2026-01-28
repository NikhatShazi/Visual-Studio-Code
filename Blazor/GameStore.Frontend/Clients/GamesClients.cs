using GameStore.Frontend.Models;
namespace GameStore.Frontend.Clients;

public class GamesClients(HttpClient httpClient)
{
  //View All Games.
  public async Task<GameSummary[]> GetGamesAsync()
  => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];
  //Add New Game.
  public async Task AddGameAsync(GameDetails game)
  => await httpClient.PostAsJsonAsync("games", game);

  //View Game Details by Id.
  public async Task<GameDetails> GetGameByIdAsync(int id)
  => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}")
     ?? throw new Exception("Game not found");

  //Edit Game.
  public async Task UpdateGameAsync(GameDetails updateGame)
  => await httpClient.PutAsJsonAsync($"games/{updateGame.Id}", updateGame);

  //Delete Game.
  public async Task DeleteGameAsync(int id)
  => await httpClient.DeleteAsync($"games/{id}");

}

