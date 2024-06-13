using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public interface IGameService
{
    public Task<List<GamePlayed>> ViewAllGamesPlayedByUser(Guid userIDFromController);

    public Task<List<GamePlayed>> ViewPlaysOfSpecificGameByUser(Guid userIDFromController, Guid gameIDFromController);

    public Task<List<Game>> GetAllGamesForUserAsync(Guid userIdFromController);

    public Task<Game> GetGameForGameId(Guid gameIdFromController);

    // public Task<string> AddNewGameToDBAsync(Game newGame);
}