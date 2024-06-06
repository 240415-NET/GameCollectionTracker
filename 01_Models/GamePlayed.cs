

using System.ComponentModel.DataAnnotations;

namespace GameCollectionTracker.Models;

public class GamePlayed
{
    [Key]
    public Guid PlayedGameID {get; set;}
    public Guid GameID {get; set;} = new();
    public string WinnerName {get; set;}
    public List<Player> Players {get;} = [];
    public List<GamePlayer> GamePlayers {get;} = [];
}