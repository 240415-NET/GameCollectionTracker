using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

public interface IGameStorageEFRepo
{
  public Task<List<Game?>> GetGamesFromDBForUserAsync (string gamertag);

    public Task<Game> GetGameFromDBByGameId (Guid gameId);
}