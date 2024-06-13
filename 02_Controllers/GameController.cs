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

    //convert this to DTO
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




