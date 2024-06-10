
namespace GameCollectionTracker.Models;

public class NewUserDTO
{
    public string GamerTag {get; set;}
    public string? Password {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public Guid? PlayerID {get; set;} = Guid.Empty;
}

public class FindPlayer
{
    public string GamerTag {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
}