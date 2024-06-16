using GameCollectionTracker.Models;
using GameCollectionTracker.Data;

namespace GameCollectionTracker.Services;

public class UserService : IUserService
{
    private readonly IEFUserStorageRepo _userStorage;

    public UserService(IEFUserStorageRepo userStorage)
    {
        _userStorage = userStorage;
    }

    public async Task<string> AddNewUserToDBAsync(NewUserDTO userInfo)
    {
        try
        {
            if (userInfo.PlayerID == Guid.Empty)
            {
                return await _userStorage.AddUserToDBWithNewPlayerAsync(userInfo);
            }
            else
            {
                return await _userStorage.AddUserToDBAndLinkPlayerAsync(userInfo);
            }
        }
        catch (Exception e)
        {
            throw new Exception($"User add failed: {e.Message}");
        }
    }

    public async Task<bool> DoesUserExistAsync(string userName)
    {
        return await _userStorage.UserRepoDoesUserExistAsync(userName);
    }

    public async Task<List<Player>> MatchingPlayersForNewUserAsync(FindPlayer userInfo)
    {
        return await _userStorage.GetMatchingPlayersForNewUser(userInfo);
    }

    public async Task<User> LoginUserAndReturnUserInfo(UserLogin userInfo)
    {
        try
        {
            return await _userStorage.LogPlayerInToApplicationAsync(userInfo);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<List<User>> GetAllUsersFromDB();
    {

    }

    public async Task<List<Player>> GetAllUnMatchedPlayersFromDB();
    {
        
    }
}