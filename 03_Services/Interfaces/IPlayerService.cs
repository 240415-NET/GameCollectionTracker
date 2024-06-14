using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

  public interface IPlayerService
    {
        public Task<string> AddPlayerAsync(Player player);
        public Task<List<Player>> GetOtherPlayers(Guid loggedInPlayerId);
    }
