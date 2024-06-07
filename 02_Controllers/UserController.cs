using GameCollectionTracker.Data;
using GameCollectionTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

}