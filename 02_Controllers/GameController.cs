using GameCollectionTracker.Data;
using GameCollectionTracker.Services;
using GameCollectionTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("/Games/")]
    public async Task<ActionResult<GameUserDTO>> GetGameByGameId(Guid gameIdToFindFromFrontEnd)
    {
        try
        {
            return await _gameService.GetGameForGameId(gameIdToFindFromFrontEnd);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("/Games/{userIdToFindFromFrontEnd}")]
    public async Task<ActionResult<GameListDTO>> GetGamesByUserId(Guid userIdToFindFromFrontEnd)
    {
        try
        {
            return await _gameService.GetAllGamesForUserAsync(userIdToFindFromFrontEnd);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("AllGamesPlayed/{playerID}")]
    public async Task<ActionResult<List<GamePlayDTO>>> ViewAllGamesPlayedByUser(Guid playerID)
    {
        try
        {
            return Ok(await _gameService.ViewAllGamesPlayedByUser(playerID));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpGet("GamesPlayed/{playerID}")]
    public async Task<ActionResult<List<GamePlayDTO>>> ViewPlaysOfSpecificGameByUser(Guid playerID, Guid GameID)
    {
        try
        {
            return Ok(await _gameService.ViewPlaysOfSpecificGameByUser(playerID, GameID));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    ///
    [HttpGet("AllGamesStats/{playerID}")]
    public async Task<ActionResult<string>> AllGamesPlayedByUserStats(Guid playerID)
    {
        try
        {
            return Ok(await _gameService.AllGamesPlayedByUserStats(playerID));
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    [HttpGet("SingleGameStats/{playerID}")]
    public async Task<ActionResult<string>> SpecificGameplayedByUserStats(Guid playerID, Guid gameID)
    {
        try
        {
            return Ok(await _gameService.SpecificGameplayedByUserStats(playerID, gameID));
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    ///
    [HttpPost]
    public async Task<IActionResult> AddNewGameToDB(Game newGame)
    {
        //newGame.GameID = new Guid();
        await _gameService.AddNewGameToDBAsync(newGame);
        return Ok("Game added");
    }

    [HttpDelete("Remove")]
    public async Task<IActionResult> DeleteGameFromDB(Guid GameId)
    {
        await _gameService.DeleteGameFromDBAsync(GameId);
        return Ok("Game Deleted");
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateGameInDB(UpdateGameDTO gameDTO)
    {
        await _gameService.UpdateGameInDBAsync(gameDTO);
        return Ok("Game Updated");
    }



    // POST: api/game/recordplay
    [HttpPost("recordplay")]
    public async Task<IActionResult> RecordGamePlay(AddGamePlayDTO gamePlayed)
    {
        try
        {
            var result = await _gameService.RecordGamePlayedAsync(gamePlayed);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error recording game play: {ex.Message}");
        }
    }

}




