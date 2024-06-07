using GameCollectionTracker.Models;

namespace GameCollectionTracker.Data;

public interface IEFUserStorageRepo
{
    public Task<bool> UserRepoDoesUserExistAsync(string userName);
    public Task<string> AddUserToDBWithNewPlayerAsync(NewUserDTO userInfo);
    public Task<string> AddUserToDBAndLinkPlayerAsync(NewUserDTO userInfo);
    public Task<List<Player>> GetMatchingPlayersForNewUser(NewUserDTO userInfo);
}

