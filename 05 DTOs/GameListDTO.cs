namespace GameCollectionTracker.Models;


public class GameListDTO
{
    List<Game> selectedGames { get; set; }
    
    public Guid UserID { get; set; }

    public string GamerTag {get; set;}
}
