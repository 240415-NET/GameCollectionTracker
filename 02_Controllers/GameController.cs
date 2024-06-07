using GameCollectionTracker.Data;
using GameCollectionTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : Controller
{
    private readonly GameContext _gameContext;

    public GameController(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    [HttpGet("/Users/{usernameToFindFromFrontEnd}")] 
    public async Task<ActionResult<Game>> GetGameByGameId(guid gameIdToFindFromFrontEnd)
    {   
        //Again, we are going to start with a try catch, so that we can NOT crash our API if something goes wrong,
        //and ideally, we can inform the front end so it can inform the user
        try
        {
            return await GameService.GetGameForGameId(gameIdToFindFromFrontEnd);
        }
        catch(Exception e)
        {
            return NotFound(e.Message);
        }
    }


}