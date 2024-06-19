using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

  public interface IPlayerService
    {
        public Task<Player> AddPlayerAsync(Player player);
        public Task<List<Player>> GetOtherPlayers(Guid loggedInPlayerId);
    }
