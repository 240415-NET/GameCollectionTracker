using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public interface IUserService
{
    public Task<string> AddNewUserToDBAsync(NewUserDTO userInfo);
    public Task<bool> DoesUserExistAsync(string userName);
    public Task<List<Player>> MatchingPlayersForNewUserAsync(NewUserDTO userInfo);
}

