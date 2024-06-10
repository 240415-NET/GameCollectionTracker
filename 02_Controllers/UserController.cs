using GameCollectionTracker.Data;
using GameCollectionTracker.Models;
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
[HttpGet("api/FindPlayers/userInfo")]
public async Task<IActionResult> GetPlayersForNewUser(string GamerTag, string FirstName, string LastName)
{
    FindPlayer userInfo = new FindPlayer(GamerTag,FirstName,LastName);
    List<Player> foundPlayers = await _userService.MatchingPlayersForNewUserAsync(userInfo);
    if(foundPlayers.Count() < 1)
    {
        return NotFound();
    }
    else
    {
        return Ok(foundPlayers);
    }
}
[HttpPost]
public async Task<IActionResult> AddNewUserToDB(NewUserDTO newUser)
{
    if(! await _userService.DoesUserExistAsync(newUser.GamerTag))
    {
        await _userService.AddNewUserToDBAsync(newUser);
        return Ok("User added"); //Probably do the login here as well
    }
    else
    {
        return BadRequest("User already exists!");
    }
}
}