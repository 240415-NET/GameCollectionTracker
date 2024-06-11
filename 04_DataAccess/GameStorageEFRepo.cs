using GameCollectionTracker.Models;
using GameCollectionTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionTracker.Data;

public class GameStorageEFRepo : IGameStorageEFRepo
{
    private readonly GameContext _context;

    //constructor needed?

    public async Task<List<Game?>> GetGamesFromDBForUserAsync (Guid userIdFromService)
    {
        return await _context.Games
            .Include(game => game.Owner)
            .Where(game => game.Owner.UserID == userIdFromService)
            .ToListAsync();

    }

    public async Task<Game> GetGameFromDBByGameId (Guid gameId)
    {
        return await _context.Games.SingleOrDefaultAsync(game => game.GameID == gameId);
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
            _context.Games.Add(gameInfo);
            await _context.SaveChangesAsync();
            return "Game added succesfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }
}

