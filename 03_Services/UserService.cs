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

    public async Task<List<UserAdminDTO>> GetAllUsersFromDB()
    {
        try
        {
            return await _userStorage.GetUsersFromDBForService();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<List<PlayerHasGamesDTO>> GetAllUnMatchedPlayersFromDB()
    {
        try
        {
            return await _userStorage.GetUnMatchedPlayersFromDBForService();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> MergePlayerRecords(MergePlayerRecordsDTO playersToMerge)
    {
        try
        {
            return await _userStorage.MergePlayersInDB(playersToMerge);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> UpdateAdminStatus(Guid userID, bool newAdminStatus)
    {
        try
        {
            return await _userStorage.UpdateAdminStatusInDB(userID, newAdminStatus);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public string CheckNextLyrics(string submission, int lineNumber)
    {
        List<string> lyrics = new();
        lyrics.Add("We're no strangers to love");
        lyrics.Add("You know the rules and so do I");
        lyrics.Add("A full commitment's what I'm thinking of");
        lyrics.Add("You wouldn't get this from any other guy");
        lyrics.Add("I just wanna tell you how I'm feeling");
        lyrics.Add("Gotta make you understand");
        lyrics.Add("Never gonna give you up");
        lyrics.Add("Never gonna let you down");
        lyrics.Add("Never gonna run around and desert you");
        lyrics.Add("Never gonna make you cry");
        lyrics.Add("Never gonna say goodbye");
        lyrics.Add("Never gonna tell a lie and hurt you");
        lyrics.Add("We've known each other for so long");
        lyrics.Add("Your heart's been aching, but you're too shy to say it");
        lyrics.Add("Inside, we both know what's been going on");
        lyrics.Add("We know the game and we're gonna play it");
        lyrics.Add("And if you ask me how I'm feeling");
        lyrics.Add("Don't tell me you're too blind to see");
        lyrics.Add("Never gonna give you up");
        lyrics.Add("Never gonna let you down");
        lyrics.Add("Never gonna run around and desert you");
        lyrics.Add("Never gonna make you cry");
        lyrics.Add("Never gonna say goodbye");
        lyrics.Add("Never gonna tell a lie and hurt you");
        lyrics.Add("Never gonna give you up");
        lyrics.Add("Never gonna let you down");
        lyrics.Add("Never gonna run around and desert you");
        lyrics.Add("Never gonna make you cry");
        lyrics.Add("Never gonna say goodbye");
        lyrics.Add("Never gonna tell a lie and hurt you");
        lyrics.Add("We've known each other for so long");
        lyrics.Add("Your heart's been aching, but you're too shy to say it");
        lyrics.Add("Inside, we both know what's been going on");
        lyrics.Add("We know the game and we're gonna play it");
        lyrics.Add("I just wanna tell you how I'm feeling");
        lyrics.Add("Gotta make you understand");
        lyrics.Add("Never gonna give you up");
        lyrics.Add("Never gonna let you down");
        lyrics.Add("Never gonna run around and desert you");
        lyrics.Add("Never gonna make you cry");
        lyrics.Add("Never gonna say goodbye");
        lyrics.Add("Never gonna tell a lie and hurt you");
        lyrics.Add("Never gonna give you up");
        lyrics.Add("Never gonna let you down");
        lyrics.Add("Never gonna run around and desert you");
        lyrics.Add("Never gonna make you cry");
        lyrics.Add("Never gonna say goodbye");
        lyrics.Add("Never gonna tell a lie and hurt you");
        lyrics.Add("Never gonna give you up");
        lyrics.Add("Never gonna let you down");
        lyrics.Add("Never gonna run around and desert you");
        lyrics.Add("Never gonna make you cry");
        lyrics.Add("Never gonna say goodbye");
        lyrics.Add("Never gonna tell a lie and hurt you");

        string goodLyrics = lyrics[lineNumber];
        string compressedLyrics = new string(goodLyrics.Where(Char.IsLetter).ToArray());
        string guessCompare = new string(submission.Where(Char.IsLetter).ToArray());
        if(compressedLyrics.ToLower() == guessCompare.ToLower())
        {
            return goodLyrics;
        }
        else
        {
            return "That's not right. You're not very good at this.";
        }
    }
}