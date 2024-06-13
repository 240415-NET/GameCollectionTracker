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

    [HttpGet("/Games/{gameIdToFindFromFrontEnd}")]
    public async Task<ActionResult<GameUserDTO>> GetGameByGameId(Guid gameIdToFindFromFrontEnd)
    {
        //Again, we are going to start with a try catch, so that we can NOT crash our API if something goes wrong,
        //and ideally, we can inform the front end so it can inform the user
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
    public async Task<ActionResult<List<Game>>> GetGamesByUserId(Guid userIdToFindFromFrontEnd)
    {
        //Again, we are going to start with a try catch, so that we can NOT crash our API if something goes wrong,
        //and ideally, we can inform the front end so it can inform the user
        try
        {
            return await _gameService.GetAllGamesForUserAsync(userIdToFindFromFrontEnd);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpGet("GamesPlayed/{userID}")]
    public async Task<ActionResult<List<GamePlayed>>> ViewPlaysOfSpecificGameByUser(Guid userID, Guid GameID)
    {
        try
        {
            return await _gameService.ViewPlaysOfSpecificGameByUser(userID, GameID);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddNewGameToDB(Game newGame)
    {
        newGame.GameID = new Guid();
        await _gameService.AddNewGameToDBAsync(newGame);
        return Ok("Game added"); 
    }

    [HttpPost("Games/DTO")]
    public async Task<IActionResult> AddNewGameDTO(NewGameDTO newGame)
    {
        await _gameService.AddNewGameDTOToDBAsync(newGame);
        return Ok("Game added"); 
    }
}




