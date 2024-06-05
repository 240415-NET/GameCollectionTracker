

namespace GameCollectionTracker.Models;

public class Player
{
    public Guid PlayerID {get; set;}
    public string PlayerName {get; set;}
    public User? AccountPlayer {get; set;}
}