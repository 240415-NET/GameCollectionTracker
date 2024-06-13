

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameCollectionTracker.Models;

public class Player
{
    [Key]
    public Guid PlayerID { get; set; }
    [MaxLength(50)]
    public string? PlayerName { get; set; }
    public bool ExistingUser { get; set; }
    [JsonIgnore]
    public List<GamePlayed> GamesPlayed { get; } = [];
    [JsonIgnore]
    public List<GamePlayer> GamePlayers { get; } = [];
}