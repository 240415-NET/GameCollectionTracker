using GameCollectionTracker.Models;
using GameCollectionTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System.Linq;

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
    public async Task<List<GamePlayDTO>> ViewAllGamesPlayedByUser(Guid playerID)
    {
        List<GamePlayDTO> returnList = new();
        Player currentPlayer = await _gameContext.Players.FirstAsync(p => p.PlayerID == playerID);
        List<GamePlayed> gamesPlayed = await _gameContext.GamesPlayed.Include(p => p.Players).Where(gp => gp.Players.Contains(currentPlayer)).ToListAsync();
        if (gamesPlayed.Count < 1)
        {
            throw new Exception("Not games played");
        }
        else
        {
            foreach(GamePlayed gameplay in gamesPlayed)
            {
                Game tempGame = await _gameContext.Games.FirstAsync(g => g.GameID == gameplay.GameID);
                User tempOwner = await _gameContext.Users.FirstAsync(u => u.UserID == tempGame.UserID);
                GamePlayDTO tempGamePlayDTO = new GamePlayDTO(gameplay, tempGame.GameName, tempOwner.GamerTag);
                returnList.Add(tempGamePlayDTO);
            }
        }
        return returnList;
    }
        public async Task<string> AllGamesPlayedByUserStats(Guid playerID)
    {
        Player currentPlayer = await _gameContext.Players.FirstAsync(p => p.PlayerID == playerID);
        List<GamePlayed> allGamesPlayed = await _gameContext.GamesPlayed.Include(p => p.Players).Where(gp => gp.Players.Contains(currentPlayer)).ToListAsync();
        List<string> gameListWinners = allGamesPlayed.Where(winner => !string.IsNullOrEmpty(winner.WinnerName)).Select(winner => winner.WinnerName).ToList();
        int playerWins = gameListWinners.Count(winner => winner == currentPlayer.PlayerName);
        int plays = allGamesPlayed.Count;

        return $"Plays: {plays} Wins: {playerWins} Win %: {(float)(playerWins/plays):P2}";
    }
    public async Task<List<GamePlayDTO>> ViewPlaysOfSpecificGameByUser(Guid playerID, Guid gameID)
    {
        List<GamePlayDTO> returnList = new();
        Player currentPlayer = await _gameContext.Players.FirstAsync(p => p.PlayerID == playerID);
        Game selectedGame = await _gameContext.Games.FirstAsync(g => g.GameID == gameID);
        User gameOwner = await _gameContext.Users.FirstAsync(u => u.UserID == selectedGame.UserID);
        List<GamePlayed> allgamesPlayed = await _gameContext.GamesPlayed.Include(p => p.Players).Where(gp => gp.Players.Contains(currentPlayer)).ToListAsync();
        List<GamePlayed> singleGameList = allgamesPlayed.Where(g => g.GameID == gameID).ToList();
        if (singleGameList.Count < 1)
        {
            throw new Exception("Not games played");
        }
        else
        {
            foreach(GamePlayed gameplay in singleGameList)
            {
                GamePlayDTO tempGamePlayDTO = new GamePlayDTO(gameplay, selectedGame.GameName, gameOwner.GamerTag);
                returnList.Add(tempGamePlayDTO);
            }
        return returnList;
        }
    }
    public async Task<string> SpecificGameplayedByUserStats(Guid playerID, Guid gameID)
    {
        Player currentPlayer = await _gameContext.Players.FirstAsync(p => p.PlayerID == playerID);
        List<GamePlayed> allgamesPlayed = await _gameContext.GamesPlayed.Include(p => p.Players).Where(gp => gp.Players.Contains(currentPlayer)).ToListAsync();
        List<GamePlayed> singleGameList = allgamesPlayed.Where(g => g.GameID == gameID).ToList();
        List<string> gameListWinners = singleGameList.Where(winner => !string.IsNullOrEmpty(winner.WinnerName)).Select(winner => winner.WinnerName).ToList();
        int playerWins = gameListWinners.Count(winner => winner == currentPlayer.PlayerName);
        int plays = singleGameList.Count;
        double winPercentage = (double)playerWins / plays;

        return $"Plays - {plays} Wins - {playerWins} Win % - {winPercentage:P2}%";
    }
    ///
    public async Task<string> AddGameToDBAsync(Game gameInfo)
    {
        try
        {
            await _gameContext.Games.AddAsync(gameInfo);
           
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
            Game? selectedGame = await _gameContext.Games.FirstOrDefaultAsync(game => game.GameID == gameId);
            if(selectedGame == null)
            {
                throw new Exception($"Game was null... {gameId}");
            }
            _gameContext.Games.Remove(selectedGame);

            if(await _gameContext.GamesPlayed.AnyAsync(game => game.GameID == gameId))
            {
            List<GamePlayed?> selectedPlays = await _gameContext.GamesPlayed.Where(game => game.GameID == gameId).ToListAsync();
            _gameContext.GamesPlayed.RemoveRange(selectedPlays);
            GamePlayed? selectedGamePlayed = await _gameContext.GamesPlayed.FirstOrDefaultAsync(game => game.GameID == gameId);
            List<GamePlayer?> selectedPlayers = await _gameContext.GamePlayers.Where(game => game.PlayedGameID == selectedGamePlayed.PlayedGameID).ToListAsync();
            _gameContext.GamePlayers.RemoveRange(selectedPlayers);
            }

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
            Game gameToUpdate = await _gameContext.Games.FirstOrDefaultAsync(game => game.GameID == gameDTO.GameID);
            if (gameDTO.GameName != null) gameToUpdate.GameName = gameDTO.GameName;
            if (gameDTO.PurchasePrice != null) gameToUpdate.PurchasePrice = (double)gameDTO.PurchasePrice;
            if (gameDTO.PurchaseDate != null) gameToUpdate.PurchaseDate = (DateOnly)gameDTO.PurchaseDate;
            if (gameDTO.MaxPlayers != null) gameToUpdate.MaxPlayers = (int)gameDTO.MaxPlayers;
            if (gameDTO.MinPlayers != null) gameToUpdate.MinPlayers = (int)gameDTO.MinPlayers;
            if (gameDTO.ExpectedGameDuration != null) gameToUpdate.ExpectedGameDuration = (int)gameDTO.ExpectedGameDuration;

            await _gameContext.SaveChangesAsync();
            return "Game updated successfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }

       
        public async Task<string> AddGamePlayedAsync(AddGamePlayDTO gamePlayed)
        {  
            GamePlayed newGamePlayed = new();
            newGamePlayed.GameID = gamePlayed.gameID;
            newGamePlayed.WinnerName = gamePlayed.winnerName;
            foreach(Guid playerID in gamePlayed.players)
            {
                Player tempPlayer = await _gameContext.Players.FirstAsync(p => p.PlayerID == playerID);
                newGamePlayed.Players.Add(tempPlayer);
            }
            await _gameContext.GamesPlayed.AddAsync(newGamePlayed);
            await _gameContext.SaveChangesAsync();
            return "Gameplay added";
        }
    
}








