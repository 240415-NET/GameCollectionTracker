
namespace GameCollectionTracker.Models;

public class GameUserDTO
{

    public Guid GameID { get; set; }
    public Guid UserID { get; set; }
    public string? GameName { get; set; }
    public double PurchasePrice { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int ExpectedGameDuration { get; set; }
    public string GamerTag {get; set;}

}

