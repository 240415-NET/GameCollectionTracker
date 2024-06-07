using GameCollectionTracker.Data;
using GameCollectionTracker.Services;
using GameCollectionTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : Controller
{
    private readonly GameContext _gameContext;

    private readonly GameService _gameService;

    public GameController(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    [HttpGet("/Users/{usernameToFindFromFrontEnd}")] 
    public async Task<ActionResult<Game>> GetGameByGameId(Guid gameIdToFindFromFrontEnd)
    {   
        //Again, we are going to start with a try catch, so that we can NOT crash our API if something goes wrong,
        //and ideally, we can inform the front end so it can inform the user
        try
        {
            return await _gameService.GetGameForGameId(gameIdToFindFromFrontEnd);
        }
        catch(Exception e)
        {
            return NotFound(e.Message);
        }
    }


}