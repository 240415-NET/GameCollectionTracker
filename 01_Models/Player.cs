

using System.ComponentModel.DataAnnotations;

namespace GameCollectionTracker.Models;

public class Player
{
    [Key]
    public Guid PlayerID { get; set; }
    [MaxLength(50)]
    public string? PlayerName { get; set; }
    public bool ExistingUser { get; set; }
    public List<GamePlayed> GamesPlayed { get; } = [];
    public List<GamePlayer> GamePlayers { get; } = [];
}