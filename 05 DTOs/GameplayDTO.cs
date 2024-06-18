

namespace GameCollectionTracker.Models;
public class GamePlayDTO
{
    public Guid PlayedGameID { get; set; }
    public Guid GameID { get; set; }
    public string WinnerName { get; set; }
    public List<Player> Players { get; } = [];
    public string gameName { get; set; }
    public string gameOwner {get; set;}



public GamePlayDTO (GamePlayed gameplay, string gameName, string gameOwner)
{
    this.PlayedGameID = gameplay.PlayedGameID;
    this.GameID = gameplay.GameID;
    this.WinnerName = gameplay.WinnerName;
    this.Players = gameplay.Players;
    this.gameName = gameName;
    this.gameOwner = gameOwner;
//     this.GamePlayers = gameplay.GamePlayers;    
//     this.playerWins = wins;
//     this.plays = plays;
}


}