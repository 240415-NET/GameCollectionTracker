

namespace GameCollectionTracker.Models;

public class Game
{
    public Guid GameID {get; set;}
    public User Owner {get; set;}
    public string GameName {get; set;}
    public double PurchaseCost {get; set;}
    public DateTime PurchaseDate {get; set;}
    public int MinPlayers {get; set;}
    public int MaxPlayers {get; set;}
    public int ExpectedGameDuration {get; set;}
}