namespace GameCollectionTracker.Models;

public class MergePlayerRecordsDTO
{
    public Guid keepPlayerID {get; set;}
    public Guid discardPlayerID {get; set;}
}

public class PlayerHasGamesDTO
{
    public Guid playerID {get; set;}
    public string playerName {get; set;}
    public bool hasGamesPlayed {get;set;}
}