using System.ComponentModel.DataAnnotations;

namespace GameCollectionTracker.Models;

public class User
{
    [Key]
    public Guid UserID { get; set; }
    [MaxLength(50)]
    public string? GamerTag { get; set; }
    public string? Password { get; set; }
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    public bool IsAdmin { get; set; }
    public List<Game> Games { get; set; } = new();
    public Player PlayerRecord { get; set; } = new();
}