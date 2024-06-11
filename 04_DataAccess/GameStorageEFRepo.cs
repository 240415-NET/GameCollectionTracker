using GameCollectionTracker.Models;
using GameCollectionTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionTracker.Data;

public class GameStorageEFRepo : IGameStorageEFRepo
{
    private readonly GameContext _context;

    //constructor needed?

    public async Task<List<Game?>> GetGamesFromDBForUserAsync (string gamertag)
    {
        return await _context.Games
            .Include(game => game.Owner)
            .Where(game => game.Owner.GamerTag == gamertag)
            .ToListAsync();


            //Here we will ask the database for all items associated with the user who's guid matches
        //the userIdFromService, using LINQ methods (and lambdas :c )

        // return await _context.Items //So we ask our context for the collection of Item objects in the database
        //     .Include(item => item.user) //We ask entity framework to also grab the associated User object from the User table
        //     .Where(item => item.user.userId == userIdFromService) //We then ask for every item who's owner's UserId matches the userIdFromService
        //     .ToListAsync(); //Finally, we turn those items into a list

    }

    public async Task<Game> GetGameFromDBByGameId (Guid gameId)
    {
        return await _context.Games.SingleOrDefaultAsync(game => game.GameID == gameId);
    }

}