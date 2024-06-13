using GameCollectionTracker.Models;
using GameCollectionTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

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
            //.Include(game => game.Owner)
            .Where(game => game.UserID == userIdFromService)
            .ToListAsync();


        //Here we will ask the database for all items associated with the user who's guid matches
        //the userIdFromService, using LINQ methods (and lambdas :c )

        // return await _context.Items //So we ask our context for the collection of Item objects in the database
        //     .Include(item => item.user) //We ask entity framework to also grab the associated User object from the User table
        //     .Where(item => item.user.userId == userIdFromService) //We then ask for every item who's owner's UserId matches the userIdFromService
        //     .ToListAsync(); //Finally, we turn those items into a list

    }

    public async Task<Game> GetGameFromDBByGameId(Guid gameId)
    {
        return await _gameContext.Games.SingleOrDefaultAsync(game => game.GameID == gameId);
        //.Include(game => game.Owner) circular dependency issue
    }
    public async Task<List<GamePlayed>> ViewAllGamesPlayedByUser(Guid playerID)
    {

        Player currentPlayer = await _gameContext.Players.FirstAsync(p => p.PlayerID == playerID);
        List<GamePlayed> gamesPlayed = await _gameContext.GamesPlayed.Include(p => p.Players).Where(gp => gp.Players.Contains(currentPlayer)).ToListAsync();
        if(gamesPlayed.Count <1)
        {
            throw new Exception("Not games played");
        }
        return gamesPlayed;
    }
    public async Task<List<GamePlayed>> ViewPlaysOfSpecificGameByUser(Guid playerID, Guid gameID)
    {
        Player currentPlayer = await _gameContext.Players.FirstAsync(p => p.PlayerID == playerID);
        List<GamePlayed> gamesPlayed = await _gameContext.GamesPlayed.Include(p => p.Players).Where(gp => gp.Players.Contains(currentPlayer)).ToListAsync();
        List<GamePlayed> returnList = gamesPlayed.Where(g => g.GameID == gameID).ToList();
        if(returnList.Count <1)
        {
            throw new Exception("Not games played");
        }        
        return returnList;
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

