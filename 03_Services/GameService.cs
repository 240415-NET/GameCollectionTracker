

using GameCollectionTracker.Data;
using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public class GameService : IGameService
{
    private readonly IGameStorageEFRepo _gameStorage;

     public GameService(IGameStorageEFRepo gameStorage)
    {
        _gameStorage = gameStorage;
    }
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
            Game? foundGame = await _gameStorage.GetGameFromDBByGameId(gameIdFromController);

            if (foundGame == null)
            {
                throw new Exception("Game not found in DB?");
            }

            return foundGame;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task<List<GamePlayed>> ViewAllGamesPlayedByUser(Guid userIDFromController)
    {

        try
        {
            List<GamePlayed>? playlist = await _gameStorage.ViewAllGamesPlayedByUser(userIDFromController);

            if (playlist.Count < 1)
            {
                throw new Exception("It doesn't appear that you've recorded any played games...");
            }

            return playlist;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task<List<GamePlayed>> ViewPlaysOfSpecificGameByUser(Guid userIDFromController, Guid gameIDFromController)
    {

        try
        {
            List<GamePlayed>? playlist = await _gameStorage.ViewPlaysOfSpecificGameByUser(userIDFromController, gameIDFromController);

            if (playlist.Count < 1)
            {
                throw new Exception("It doesn't appear that you've recorded any played games...");
            }

            return playlist;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    // public async Task<string> AddNewGameToDBAsync(Game newGame)
    // {
    //     try
    //     {

    //             return await _gameStorage.AddGameToDBAsync(newGame);
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception($"Game add failed: {e.Message}");
    //     }
    // }
}