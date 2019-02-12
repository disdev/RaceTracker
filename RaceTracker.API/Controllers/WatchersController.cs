using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RaceTracker.Data;
using RaceTracker.Data.Models;

namespace RaceTracker.Controllers
{
    [Route("api/watchers")]
    [ApiController]
    public class WatchersController : ControllerBase
    {
        private readonly RaceTrackerDataService dataService;

        public WatchersController(RaceTrackerDataService service)
        {
            dataService = service;
        }

        [HttpPost]
        public async Task<Watcher> Subscribe([FromQuery] string raceNumber, [FromQuery] string phoneNumber)
        {
            return await dataService.Subscribe(raceNumber, phoneNumber);
        }
    }
}
