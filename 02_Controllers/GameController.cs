
using GameCollectionTracker.Data;
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

}