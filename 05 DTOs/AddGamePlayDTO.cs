namespace GameCollectionTracker.Models;

public class AddGamePlayDTO
{
    public Guid gameID { get; set; }
    public string winnerName { get; set; }
    public List<Guid> players { get; set; }
}