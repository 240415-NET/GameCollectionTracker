namespace GameCollectionTracker.Models;

public class UserAdminDTO
{
    public Guid UserID { get; set; }
    public Guid PlayerID {get; set;}
    public string? GamerTag { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsAdmin { get; set; }
}