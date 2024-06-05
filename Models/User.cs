

namespace GameCollectionTracker.Models;

public class User
{
    public Guid UserID {get; set;}
    public string GamerTag {get; set;}
    public string Password {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public bool IsAdmin {get; set;}
    public List<Game> Games {get; set;} = [];
}