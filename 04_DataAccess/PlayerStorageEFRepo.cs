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
               // return null;
           }

            playerToAdd.PlayerID = Guid.NewGuid();
            playerToAdd.ExistingUser = false;
              
            _context.Players.Add(playerToAdd);
            await _context.SaveChangesAsync();

            return $"Player '{playerToAdd.PlayerName}' added successfully!";
           // return playerToAdd;
        }


    }
