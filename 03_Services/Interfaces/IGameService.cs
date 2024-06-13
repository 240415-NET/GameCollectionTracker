using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public interface IGameService
{
    public Task<List<GamePlayed>> ViewAllGamesPlayedByUser(Guid userIDFromController);

    public Task<List<GamePlayed>> ViewPlaysOfSpecificGameByUser(Guid userIDFromController, Guid gameIDFromController);

}