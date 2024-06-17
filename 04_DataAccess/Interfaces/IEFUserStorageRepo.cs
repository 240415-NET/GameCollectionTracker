using GameCollectionTracker.Models;

namespace GameCollectionTracker.Data;

public interface IEFUserStorageRepo
{
    public Task<bool> UserRepoDoesUserExistAsync(string userName);
    public Task<string> AddUserToDBWithNewPlayerAsync(NewUserDTO userInfo);
    public Task<string> AddUserToDBAndLinkPlayerAsync(NewUserDTO userInfo);
    public Task<List<Player>> GetMatchingPlayersForNewUser(FindPlayer userInfo);
    public Task<User> LogPlayerInToApplicationAsync(UserLogin userInfo);
    public  Task<List<UserAdminDTO>> GetUsersFromDBForService();
    public Task<List<PlayerHasGamesDTO>> GetUnMatchedPlayersFromDBForService();
    public Task<string> UpdateAdminStatusInDB(Guid userID, bool newAdminStatus);
    public Task<string> MergePlayersInDB(MergePlayerRecordsDTO playersToMerge);
}

