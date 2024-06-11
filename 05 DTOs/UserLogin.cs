namespace GameCollectionTracker.Models;

public class UserLogin
{
    public string UserName {get; set;}
    public string UserPass {get; set;}

    public UserLogin (string UserName, string UsersPass)
    {
        this.UserName = UserName;
        this.UserPass = UsersPass;
    }
}