using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

  public interface IPlayerService
    {
        Task<string> AddPlayerAsync(Player player);
    }
