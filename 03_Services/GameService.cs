

using GameCollectionTracker.Data;
using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public class GameService
{
    private readonly GameStorageEFRepo _gameStorage;
    public async Task<List<Game>> GetAllGamesForUserAsync(string gamertagFromController)
    {
        List<Game> foundGames = new();

        // //We know we will get something back from the data access layer
        // //I've got some assumptions about what it is, but lets say I'm a little lazy
        // //We can leverage "var" to make things easier for us 

        var resultList = await _gameStorage.GetGamesFromDBForUserAsync(gamertagFromController);

        foreach (var game in resultList)
        {
            //For each item model object in our result list, we will call that mapping constructor 
            //that takes an item and uses it to create an ItemDTO for us. Then it adds that new ItemDTO object
            //to the foundItems list we created above.
            foundGames.Add(game);
        }

        return foundGames;
    }

    public async Task<Game> GetGameForGameId(Guid gameIdFromController)
    {

        try
        {   
            Game? foundGame =  await _gameStorage.GetGameFromDBByGameId(gameIdFromController);

            if(foundGame == null)
            {
                throw new Exception("Game not found in DB?");
            }

            return foundGame;
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}