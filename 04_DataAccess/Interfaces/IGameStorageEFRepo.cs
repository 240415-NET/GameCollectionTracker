using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

public interface IGameStorageEFRepo
{
  public Task<List<Game?>> GetGamesFromDBForUserAsync(string gamertag);

  public Task<Game> GetGameFromDBByGameId(Guid gameId);

  public Task<List<GamePlayed>> ViewAllGamesPlayedByUser(Guid userID);

  public Task<List<GamePlayed>> ViewPlaysOfSpecificGameByUser(Guid userID, Guid gameID);
}