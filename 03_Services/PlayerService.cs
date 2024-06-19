using GameCollectionTracker.Data;
using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;


    public class PlayerService : IPlayerService
    {
        private readonly IPlayerStorageEFRepo _playerStorageEFRepo;

        public PlayerService(IPlayerStorageEFRepo playerStorageEFRepo)
        {
            _playerStorageEFRepo = playerStorageEFRepo;
        }


        public async Task<Player> AddPlayerAsync(Player player)
        {
             Player result = await _playerStorageEFRepo.AddPlayerAsync(player);
             return result;

        }

        
        public async Task<List<Player>> GetOtherPlayers(Guid loggedInPlayerId)
        {

        try
        {
            return await _playerStorageEFRepo.GetAllPlayersExcept(loggedInPlayerId);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        }
    }
