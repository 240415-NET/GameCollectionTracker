
using GameCollectionTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionTracker.Data;

public class EFUserStorageRepo : IEFUserStorageRepo
{
    private readonly GameContext _gameContext;

    public EFUserStorageRepo(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public async Task<bool> UserRepoDoesUserExistAsync(string userName)
    {
        return await _gameContext.Users.AnyAsync(user => user.GamerTag == userName);
    }

    public async Task<string> AddUserToDBWithNewPlayerAsync(NewUserDTO userInfo)
    {
        try
        {
            Player newPlayer = new();
            newPlayer.PlayerID = Guid.NewGuid();
            newPlayer.PlayerName = userInfo.GamerTag;
            newPlayer.ExistingUser = true;
            User newUser = new();
            newUser.UserID = Guid.NewGuid();
            newUser.GamerTag = userInfo.GamerTag;
            newUser.FirstName = userInfo.FirstName;
            newUser.LastName = userInfo.LastName;
            newUser.IsAdmin = false;
            //currently clear text with no encryption.  Need to implement method to encrypt pw for storage and another to decrypt for verification
            newUser.Password = userInfo.Password;
            newUser.PlayerRecord = newPlayer;
            _gameContext.Users.Add(newUser);
            await _gameContext.SaveChangesAsync();
            return "User added succesfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }

    public async Task<string> AddUserToDBAndLinkPlayerAsync(NewUserDTO userInfo)
    {
        try
        {
            Player playerToLink = _gameContext.Players.First(p => p.PlayerID == userInfo.PlayerID);
            User newUser = new();
            newUser.UserID = Guid.NewGuid();
            newUser.GamerTag = userInfo.GamerTag;
            newUser.FirstName = userInfo.FirstName;
            newUser.LastName = userInfo.LastName;
            newUser.IsAdmin = false;
            //currently clear text with no encryption.  Need to implement method to encrypt pw for storage and another to decrypt for verification
            newUser.Password = userInfo.Password;
            playerToLink.PlayerName = userInfo.GamerTag;
            playerToLink.ExistingUser = true;
            newUser.PlayerRecord = playerToLink;
            _gameContext.Users.Add(newUser);
            await _gameContext.SaveChangesAsync();
            return "User added succesfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }

    public async Task<List<Player>> GetMatchingPlayersForNewUser(FindPlayer userInfo)
    {
        List<Player> foundPlayers = new();
        try
        {
            List<Player> unaffiliatedPlayers = await _gameContext.Players.Where(e => e.ExistingUser == false).ToListAsync();
            if (unaffiliatedPlayers.Count() < 1)
            {
                return foundPlayers;
            }
            else
            {
                foreach (Player player in unaffiliatedPlayers)
                {
                    if (player.PlayerName == userInfo.GamerTag || player.PlayerName == userInfo.FirstName || player.PlayerName == userInfo.LastName || (player.PlayerName.Contains(userInfo.FirstName) && player.PlayerName.Contains(userInfo.LastName)))
                    {
                        foundPlayers.Add(player);
                    }
                }
                return foundPlayers;
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Something went wrong... {e.Message}");
        }
    }

    public async Task<User> LogPlayerInToApplicationAsync(UserLogin userInfo)
    {
        User foundUser = await _gameContext.Users.Include(p => p.PlayerRecord).FirstAsync(e => e.GamerTag == userInfo.userName);
        if (foundUser.Password != userInfo.userPass)
        {
            throw new Exception("Invalid Password!");
        }
        else
        {
            return foundUser;
        }

    }

    public async Task<List<User>> GetUsersFromDBForService()
    {
        try
        {
            List<User> userList = await _gameContext.Users.AsNoTracking().ToListAsync();
            return userList;
        }
        catch (Exception e)
        {
            throw new Exception($"Database read error: {e.Message}");
        }
    }

    public async Task<List<PlayerHasGamesDTO>> GetUnMatchedPlayersFromDBForService()
    {
        try
        {
            List<PlayerHasGamesDTO> returnList = new();
            List<Player> playerList = await _gameContext.Players.Where(p => p.ExistingUser == false).ToListAsync();
            for(int i=0; i < playerList.Count; i++)
            {
                if(await _gameContext.GamesPlayed.Include(p => p.Players).AnyAsync(pl => pl.Players.Contains(playerList[i])))
                {
                    PlayerHasGamesDTO playerToAdd = new() {
                        playerID = playerList[i].PlayerID,
                        playerName = playerList[i].PlayerName,
                        hasGamesPlayed = true
                    };
                    returnList.Add(playerToAdd);
                }
                else
                {
                    PlayerHasGamesDTO playerToAdd = new() {
                        playerID = playerList[i].PlayerID,
                        playerName = playerList[i].PlayerName,
                        hasGamesPlayed = false
                    };
                    returnList.Add(playerToAdd);
                }
            }
            return returnList;
        }
        catch (Exception e)
        {
            throw new Exception($"Database read error: {e.Message}");
        }
    }

    public async Task<string> MergePlayersInDB(MergePlayerRecordsDTO playersToMerge)
    {
        try
        {
            Player playerToRemove = await _gameContext.Players.FirstAsync(p => p.PlayerID == playersToMerge.discardPlayerID);
            List<GamePlayed> removePlayersGames = await _gameContext.GamesPlayed.Include(p => p.Players).Where(pl => pl.Players.Contains(playerToRemove)).ToListAsync();
            if (removePlayersGames.Count > 1)
            {
                for (int i = 0; i < removePlayersGames.Count; i++)
                {
                    for (int j = 0; j < removePlayersGames[i].Players.Count; j++)
                    {
                        if (removePlayersGames[i].Players[j].PlayerID == playersToMerge.discardPlayerID)
                        {
                            removePlayersGames[i].Players[j].PlayerID = playersToMerge.keepPlayerID;
                        }
                    }
                }
            }
            await _gameContext.SaveChangesAsync();
            _gameContext.Players.Remove(playerToRemove);
            await _gameContext.SaveChangesAsync();
            return "Player Record Updated";
        }
        catch (Exception e)
        {
            throw new Exception($"Database update failed: {e.Message}");
        }
    }

    public async Task<string> UpdateAdminStatusInDB(Guid userID, bool newAdminStatus)
    {
        try
        {
            User userToUpdate = await _gameContext.Users.FirstAsync(u => u.UserID == userID);
            userToUpdate.IsAdmin = newAdminStatus;
            await _gameContext.SaveChangesAsync();
            return "User updated";
        }
        catch (Exception e)
        {
            throw new Exception($"Database update error: {e.Message}");
        }
    }
}