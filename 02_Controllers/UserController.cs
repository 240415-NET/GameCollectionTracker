using GameCollectionTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly GameContext _gameContext;

    public UserController(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

}