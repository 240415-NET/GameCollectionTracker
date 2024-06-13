

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameCollectionTracker.Models;

public class Game
{
    [Key]
    public Guid GameID { get; set; }
    public Guid UserID { get; set; }
    [MaxLength(50)]
    public string? GameName { get; set; }
    public double PurchasePrice { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int ExpectedGameDuration { get; set; }
}