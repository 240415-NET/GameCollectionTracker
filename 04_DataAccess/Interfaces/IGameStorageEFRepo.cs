using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

public interface IGameStorageEFRepo
{
  public Task<List<GamePlayed>> ViewAllGamesPlayedByUser(Guid playerID);

  public Task<List<GamePlayed>> ViewPlaysOfSpecificGameByUser(Guid playerID, Guid gameID);

  public Task<GameListDTO> GetGamesFromDBForUserAsync(Guid userIdFromService);

  public Task<GameUserDTO> GetGameFromDBByGameId(Guid gameId);

  public Task<string> AddGameToDBAsync(Game gameInfo);

  public Task<string> DeleteGameFromDBAsync(Guid gameId);

  public Task<string> UpdateGameInDBAsync(UpdateGameDTO gameDTO);

}