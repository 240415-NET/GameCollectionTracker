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
    public async Task<List<GamePlayed>> ViewAllGamesPlayedByUser(Guid userID)
    {

        User currentUser = await _gameContext.Users.FirstAsync(cu => cu.UserID == userID);

        Player currentPlayer = await _gameContext.Players
        .Include(pd => pd.GamesPlayed)
        .FirstAsync(cp => cp.PlayerID == currentUser.PlayerRecord.PlayerID);

        return currentPlayer.GamesPlayed;
    }
    public async Task<List<GamePlayed>> ViewPlaysOfSpecificGameByUser(Guid userID, Guid gameID)
    {

        User currentUser = await _gameContext.Users.FirstAsync(cu => cu.UserID == userID);
        Game currentGame = await _gameContext.Games.FirstAsync(cg => cg.GameID == gameID);

        Player currentPlayer = await _gameContext.Players
        .Include(pd => pd.GamesPlayed).FirstAsync(cp => cp.PlayerID == currentUser.PlayerRecord.PlayerID);

        return currentPlayer.GamesPlayed;
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

}

