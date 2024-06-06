

namespace GameCollectionTracker.Models;

public class GamePlayer
{
    public Guid? PlayerID {get; set;}
    public Guid? PlayedGameID {get; set;} 
    public GamePlayed? PlayedGame {get; set;} = new();
    public Player? PlayerOfGame {get; set;} = new();
}