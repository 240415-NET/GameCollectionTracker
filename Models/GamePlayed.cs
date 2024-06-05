

namespace GameCollectionTracker.Models;

public class GamePlayed
{
    public Guid PlayedGameID {get; set;}
    public Game PlayedGame {get; set;}
    public Player GameWinner {get; set;}
    public List<GamePlayer> GamePlayers {get; set;} = [];
}