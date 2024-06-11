
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
        catch(Exception e)
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
            if(unaffiliatedPlayers.Count() < 1)
            {
                return foundPlayers;
            }
            else
            {
                foreach(Player player in unaffiliatedPlayers)
                {
                    if(player.PlayerName == userInfo.GamerTag || player.PlayerName == userInfo.FirstName || player.PlayerName == userInfo.LastName || (player.PlayerName.Contains(userInfo.FirstName) && player.PlayerName.Contains(userInfo.LastName)))
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
        User foundUser = await _gameContext.Users.FirstAsync(e => e.GamerTag == userInfo.UserName);
        if(foundUser.Password != userInfo.UserPass)
        {
            throw new Exception("Invalid Password!");
        }
        else
        {
            return foundUser;
        }

    }
}