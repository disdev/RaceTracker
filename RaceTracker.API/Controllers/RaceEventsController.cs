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
    [Route("api/events")]
    public class RaceEventsController : Controller
    {
        private readonly RaceTrackerDataService dataService;

        public RaceEventsController(RaceTrackerContext context, RaceTrackerDataService svc)
        {
            dataService = svc;
        }

        // GET: api/events
        [HttpGet]
        public async Task<List<RaceEvent>> GetRaceEvents()
        {
            return await dataService.GetRaceEvents();
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<RaceEvent> GetRaceEventById([FromRoute] Guid id)
        {
            return await dataService.GetRaceEvent(id);
        }

        // POST: api/events
        [HttpPost]
        public async Task<RaceEvent> PostRaceEvent([FromBody] RaceEvent raceEvent)
        {
            return await dataService.AddRaceEvent(raceEvent);
        }
    }
}