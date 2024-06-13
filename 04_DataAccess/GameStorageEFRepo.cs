using GameCollectionTracker.Models;
using GameCollectionTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionTracker.Data;

public class GameStorageEFRepo : IGameStorageEFRepo
{
    private readonly GameContext _gameContext;

    public GameStorageEFRepo(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public async Task<List<Game?>> GetGamesFromDBForUserAsync (Guid userIdFromService)
    {
        return await _gameContext.Games
            .Include(game => game.Owner)
            .Where(game => game.Owner.UserID == userIdFromService)
            .ToListAsync();
    }

    public async Task<Game> GetGameFromDBByGameId (Guid gameId)
    {
        return await _gameContext.Games.SingleOrDefaultAsync(game => game.GameID == gameId);
        //.Include(game => game.Owner) circular dependency issue
    }

    public async Task<string> AddGameToDBAsync(Game gameInfo)
    {
        try
        {
            // Game newGame = new();
            gameInfo.GameID = Guid.NewGuid();
            // newGame.Owner = userId;
            // newGame.GameName = gameInfo.GameName;
            // newGame.PurchasePrice = gameInfo.PurchasePrice;
            // newGame.PurchaseDate = 
            // newGame.MinPlayers = 
            // newGame.MaxPlayers = 
            // newGame.ExpectedGameDuration = 
            _gameContext.Games.Add(gameInfo);
            await _gameContext.SaveChangesAsync();
            return "Game added succesfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }
}

