using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

    public interface IPlayerStorageEFRepo
    {
        Task<string> AddPlayerAsync(Player player);
    }
