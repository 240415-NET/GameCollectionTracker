using GameCollectionTracker.Models;
using GameCollectionTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionTracker.Data;


    public class PlayerStorageEFRepo : IPlayerStorageEFRepo
    {
        private readonly GameContext _context;

        public PlayerStorageEFRepo(GameContext context)
        {
            _context = context;
        }


        public async Task<string> AddPlayerAsync(Player playerToAdd)
        {
           bool userExists = await _context.Users.AnyAsync(u => u.GamerTag == playerToAdd.PlayerName);
           if (userExists)
           {
               return $"Player '{playerToAdd.PlayerName}' already exists";
           }

            playerToAdd.PlayerID = Guid.NewGuid();
            playerToAdd.ExistingUser = false;
              
            _context.Players.Add(playerToAdd);
            await _context.SaveChangesAsync();

            return $"Player '{playerToAdd.PlayerName}' added successfully!";
        }

         
    public async Task<List<Player>> GetAllPlayersExcept(Guid loggedInPlayerId)
    {
        return await _context.Players.Where(player => player.PlayerID != loggedInPlayerId).ToListAsync();
        
    }


    }
