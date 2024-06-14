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


    public async Task<GameListDTO> GetGamesFromDBForUserAsync(Guid userIdFromService)
    {
        GameListDTO resultDTO = new();
        resultDTO.selectedGames = await _gameContext.Games
            //.Include(game => game.Owner)
            .Where(game => game.UserID == userIdFromService)
            .ToListAsync();

        User selectedUser = await _gameContext.Users.SingleAsync(user => user.UserID == userIdFromService);
        resultDTO.UserID = selectedUser.UserID;
        resultDTO.GamerTag = selectedUser.GamerTag;

        //return GameListDTO
        return resultDTO;
    }

    public async Task<GameUserDTO> GetGameFromDBByGameId(Guid gameId)
    {
        GameUserDTO newDTO = new();
        Game selectedGame = await _gameContext.Games.SingleOrDefaultAsync(game => game.GameID == gameId);
        User selectedUser = await _gameContext.Users.SingleAsync(user => user.UserID == selectedGame.UserID);


        newDTO.GameName = selectedGame.GameName;
        newDTO.UserID = selectedGame.UserID;
        newDTO.PurchasePrice = selectedGame.PurchasePrice;
        newDTO.PurchaseDate = selectedGame.PurchaseDate;
        newDTO.MinPlayers = selectedGame.MinPlayers;
        newDTO.MaxPlayers = selectedGame.MaxPlayers;
        newDTO.ExpectedGameDuration = selectedGame.ExpectedGameDuration;
        newDTO.GamerTag = selectedUser.GamerTag;
        return newDTO;

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
            User currentUser = await _gameContext.Users.FirstAsync(user => user.UserID == gameInfo.UserID);
            currentUser.Games.Add(gameInfo);
           
            await _gameContext.SaveChangesAsync();
            return "Game added succesfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }

    public async Task<string> DeleteGameFromDBAsync(Guid gameId)
    {
        try
        {
            Game selectedGame = await _gameContext.Games.FirstOrDefaultAsync(game => game.GameID == gameId);
            _gameContext.Games.Remove(selectedGame);
           
            await _gameContext.SaveChangesAsync();
            return "Game deleted succesfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }

    public async Task<string> UpdateGameInDBAsync(UpdateGameDTO gameDTO)
    {
        try
        {
            Game gameToUpdate= await _gameContext.Games.FirstOrDefaultAsync(game => game.GameID == gameDTO.GameID);
            if(gameDTO.GameName != null) gameToUpdate.GameName = gameDTO.GameName;
            if(gameDTO.PurchasePrice != null) gameToUpdate.PurchasePrice = (double) gameDTO.PurchasePrice;
            if(gameDTO.PurchaseDate != null) gameToUpdate.PurchaseDate = (DateOnly) gameDTO.PurchaseDate;
            if(gameDTO.MaxPlayers != null) gameToUpdate.MaxPlayers = (int) gameDTO.MaxPlayers;
            if(gameDTO.MinPlayers != null) gameToUpdate.MinPlayers = (int)gameDTO.MinPlayers;
            if(gameDTO.ExpectedGameDuration != null) gameToUpdate.ExpectedGameDuration = (int)gameDTO.ExpectedGameDuration;

            await _gameContext.SaveChangesAsync();
            return "Game updated successfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }
}

