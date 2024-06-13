

using System.Text.Json.Serialization;

namespace GameCollectionTracker.Models;

public class GamePlayer
{
    public Guid? PlayerID {get; set;}
    public Guid? PlayedGameID {get; set;} 
    [JsonIgnore]
    public GamePlayed? PlayedGame {get; set;} = new();
    [JsonIgnore]
    public Player? PlayerOfGame {get; set;} = new();
}