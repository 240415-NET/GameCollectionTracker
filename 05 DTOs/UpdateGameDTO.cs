
namespace GameCollectionTracker.Models;

public class UpdateGameDTO
{
    public Guid GameID {get; set; }
    public string? GameName { get; set; }
    public double? PurchasePrice { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    public int? MinPlayers { get; set; }
    public int? MaxPlayers { get; set; }
    public int? ExpectedGameDuration { get; set; }

}