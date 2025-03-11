using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission10_Godwin.Data;
using Mission10_Godwin.DTO;
using Mission10_Godwin.Models;

namespace Mission10_Godwin.Controllers
{
    [Route("api/[controller]")] // Basic route for API
    [ApiController] // Indicates that the controller is an API controller instead of a regular controler
    public class BowlerController : ControllerBase
    {
        private DbContextBowling _context;

        public BowlerController (DbContextBowling context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetBowlers")] // The below method will handle get requests
        public async Task<ActionResult<IEnumerable<BowlerDto>>> GetBowlers() // Once finished, it returns an enumerable result of type BowlerDTO
        {
            var bowlers = await _context.Bowlers
                .Include(b => b.Team) // Join with the Teams table
                .Where(b => b.Team != null && (b.Team.TeamName == "Marlins" || b.Team.TeamName == "Sharks")) // Filter for only sharks or marlins teams
                .Select(b => new BowlerDto
                {
                    BowlerId = b.BowlerId,
                    BowlerLastName = b.BowlerLastName,
                    BowlerFirstName = b.BowlerFirstName,
                    BowlerMiddleInit = b.BowlerMiddleInit,
                    BowlerAddress = b.BowlerAddress,
                    BowlerCity = b.BowlerCity,
                    BowlerState = b.BowlerState,
                    BowlerZip = b.BowlerZip,
                    BowlerPhoneNumber = b.BowlerPhoneNumber,
                    TeamName = b.Team.TeamName // Access the team table to get the team name
                })
                .ToListAsync();

            return Ok(bowlers);
        }
    }
}
