using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public interface IGameService
{
    public Task<List<GamePlayDTO>> ViewAllGamesPlayedByUser(Guid playerIDFromController);

    public Task<List<GamePlayDTO>> ViewPlaysOfSpecificGameByUser(Guid playerIDFromController, Guid gameIDFromController);

    public Task<string> SpecificGameplayedByUserStats(Guid playerID, Guid gameID);

    public Task<string> AllGamesPlayedByUserStats(Guid playerID);

    public Task<GameListDTO> GetAllGamesForUserAsync(Guid userIdFromController);

    public Task<GameUserDTO> GetGameForGameId(Guid gameIdFromController);

    public Task<string> AddNewGameToDBAsync(Game newGame);

    public Task<string> DeleteGameFromDBAsync(Guid gameId);

    public Task<string> UpdateGameInDBAsync(UpdateGameDTO updatedGame);

    public Task<string> RecordGamePlayedAsync(AddGamePlayDTO gamePlayed);

}