

using GameCollectionTracker.Data;
using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public class GameService : IGameService
{
    private readonly GameStorageEFRepo _gameStorage;
    public async Task<List<Game>> GetAllGamesForUserAsync(Guid userIdFromController)
    {
        List<Game> foundGames = new();

        var resultList = await _gameStorage.GetGamesFromDBForUserAsync(userIdFromController);

        foreach (var game in resultList)
        {
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

    public async Task<string> AddNewGameToDBAsync(Game newGame)
    {
        try
        {

                return await _gameStorage.AddGameToDBAsync(newGame);
        }
        catch (Exception e)
        {
            throw new Exception($"Game add failed: {e.Message}");
        }
    }
}