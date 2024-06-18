using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

public interface IGameStorageEFRepo
{
  public Task<List<GamePlayDTO>> ViewAllGamesPlayedByUser(Guid playerID);

  public Task<List<GamePlayDTO>> ViewPlaysOfSpecificGameByUser(Guid playerID, Guid gameID);

  public Task<string> SpecificGameplayedByUserStats(Guid playerID, Guid gameID);

  public Task<string> AllGamesPlayedByUserStats(Guid playerID);

  public Task<GameListDTO> GetGamesFromDBForUserAsync(Guid userIdFromService);

  public Task<GameUserDTO> GetGameFromDBByGameId(Guid gameId);

  public Task<string> AddGameToDBAsync(Game gameInfo);

  public Task<string> DeleteGameFromDBAsync(Guid gameId);

  public Task<string> UpdateGameInDBAsync(UpdateGameDTO gameDTO);

  public Task AddGamePlayedAsync(GamePlayed gamePlayed);
}