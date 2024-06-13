using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

public interface IGameStorageEFRepo
{
  public Task<List<Game?>> GetGamesFromDBForUserAsync (Guid userIdFromService);

  public Task<Game> GetGameFromDBByGameId (Guid gameId);
}