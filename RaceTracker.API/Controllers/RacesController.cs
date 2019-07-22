using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceTracker.Data;
using RaceTracker.Data.Models;

namespace RaceTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/Races")]
    public class RacesController : Controller
    {
        private readonly RaceTrackerDataService dataService;

        public RacesController(RaceTrackerDataService svc)
        {
            dataService = svc;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<List<Race>> GetRaces()
        {
            return await dataService.GetRaces();
        }

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetRaceByRaceEvent([FromRoute] Guid raceEventId)
        {
            var races = await dataService.GetRacesByRaceEvent(raceEventId);
            return Ok(races);
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRaceById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var race = await dataService.GetRace(id);

            if (race == null)
            {
                return NotFound();
            }

            return Ok(race);
        }

        // POST: api/Races
        [HttpPost]
        public async Task<IActionResult> PostRace([FromBody] Race race)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataService.AddRace(race);
            
            return CreatedAtAction("GetRace", new { id = race.Id }, race);
        }
    }
}