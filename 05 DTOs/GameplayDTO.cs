

namespace GameCollectionTracker.Models;
public class GamePlayDTO
{
    public Guid PlayedGameID { get; set; }
    public Guid GameID { get; set; }
    public string WinnerName { get; set; }
    public List<Player> Players { get; } = [];
    //public List<GamePlayer> GamePlayers { get; } = [];
    public string gameName { get; set; }
    // public int playerWins  { get; set; }
    // public int plays  { get; set; }


public GamePlayDTO (GamePlayed gameplay, string gameName)
{
    this.PlayedGameID = gameplay.PlayedGameID;
    this.GameID = gameplay.GameID;
    this.WinnerName = gameplay.WinnerName;
    this.Players = gameplay.Players;
    this.gameName = gameName;
//     this.GamePlayers = gameplay.GamePlayers;    
//     this.playerWins = wins;
//     this.plays = plays;
}


}