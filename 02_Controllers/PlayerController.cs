using GameCollectionTracker.Data;
using GameCollectionTracker.Services;
using GameCollectionTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionTracker.Controllers;

    [ApiController] 
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // POST: api/player
        [HttpPost("AddPlayer")]
        public async Task<IActionResult> AddPlayer(Player player)
        {
                
            Player addedPlayer= await _playerService.AddPlayerAsync(player); 

            return Ok(addedPlayer);
              
        }

        //Get
        [HttpGet("otherplayers/{loggedInPlayerId}")]
        public async Task<ActionResult<Player>> GetOtherPlayers(Guid loggedInPlayerId)
        {
        try
        {
            var otherPlayers = await _playerService.GetOtherPlayers(loggedInPlayerId);
            return Ok(otherPlayers);
            
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
               
        }

      
    



