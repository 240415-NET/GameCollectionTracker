using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public interface IGameService
{
    public Task<List<Game>> GetAllGamesForUserAsync(Guid userIdFromController);

    public Task<Game> GetGameForGameId(Guid gameIdFromController);

    public Task<string> AddNewGameToDBAsync(Game newGame);
}