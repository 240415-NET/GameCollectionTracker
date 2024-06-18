

using GameCollectionTracker.Data;
using GameCollectionTracker.Models;
using Microsoft.IdentityModel.Tokens;

namespace GameCollectionTracker.Services;

public class GameService : IGameService
{
    private readonly IGameStorageEFRepo _gameStorage;

    public GameService(IGameStorageEFRepo gameStorage)
    {
        _gameStorage = gameStorage;
    }
    public async Task<GameListDTO> GetAllGamesForUserAsync(Guid userIdFromController)
    {
        return await _gameStorage.GetGamesFromDBForUserAsync(userIdFromController);
    }

    public async Task<GameUserDTO> GetGameForGameId(Guid gameIdFromController)
    {

        try
        {
            GameUserDTO? foundGame = await _gameStorage.GetGameFromDBByGameId(gameIdFromController);

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
    public async Task<List<GamePlayDTO>> ViewAllGamesPlayedByUser(Guid playerIDFromController)
    {
        try
        {
            List<GamePlayDTO>? playlist = await _gameStorage.ViewAllGamesPlayedByUser(playerIDFromController);

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
    public async Task<List<GamePlayDTO>> ViewPlaysOfSpecificGameByUser(Guid playerIDFromController, Guid gameIDFromController)
    {

        try
        {
            List<GamePlayDTO>? playlist = await _gameStorage.ViewPlaysOfSpecificGameByUser(playerIDFromController, gameIDFromController);

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
    ///
    public async Task<string> AllGamesPlayedByUserStats(Guid playerIDFromController)
    {
        try
        {
            string? gameStats = await _gameStorage.AllGamesPlayedByUserStats(playerIDFromController);

            if (gameStats.IsNullOrEmpty())
            {
                throw new Exception("It doesn't appear that you've recorded any played games...");
            }

            return gameStats;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task<string> SpecificGameplayedByUserStats(Guid playerIDFromController, Guid gameIDFromController)
    {
        try
        {
            string? gameStats = await _gameStorage.SpecificGameplayedByUserStats(playerIDFromController, gameIDFromController);

            if (gameStats.IsNullOrEmpty())
            {
                throw new Exception("It doesn't appear that you've recorded any played games...");
            }

            return gameStats;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    ///
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

    public async Task<string> DeleteGameFromDBAsync(Guid gameId)
    {
        try
        {
            return await _gameStorage.DeleteGameFromDBAsync(gameId);
        }
        catch (Exception e)
        {
            throw new Exception($"Game add failed: {e.Message}");
        }
    }

    public async Task<string> UpdateGameInDBAsync(UpdateGameDTO gameDTO)
    {
        try
        {
            return await _gameStorage.UpdateGameInDBAsync(gameDTO);
        }
        catch (Exception e)
        {
            throw new Exception($"Game add failed: {e.Message}");
        }
    }
}
