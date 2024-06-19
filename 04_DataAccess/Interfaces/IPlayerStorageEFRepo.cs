using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

    public interface IPlayerStorageEFRepo
    {
        public Task<Player> AddPlayerAsync(Player player);
        public Task<List<Player>> GetAllPlayersExcept(Guid loggedInPlayerId);

    }
