using GameCollectionTracker.Models;

namespace GameCollectionTracker.Services;

public interface IUserService
{
    public Task<string> AddNewUserToDBAsync(NewUserDTO userInfo);
    public Task<bool> DoesUserExistAsync(string userName);
    public Task<List<Player>> MatchingPlayersForNewUserAsync(FindPlayer userInfo);
    public Task<User> LoginUserAndReturnUserInfo(UserLogin userInfo);
    public Task<List<User>> GetAllUsersFromDB();
    public Task<List<PlayerHasGamesDTO>> GetAllUnMatchedPlayersFromDB();
    public Task<string> MergePlayerRecords(MergePlayerRecordsDTO playersToMerge);
    public Task<string> UpdateAdminStatus(Guid userID, bool newAdminStatus);
}

