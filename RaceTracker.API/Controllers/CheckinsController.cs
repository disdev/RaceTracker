using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaceTracker.Data;
using RaceTracker.Data.Models;

namespace RaceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinsController : ControllerBase
    {
        private readonly RaceTrackerDataService dataService;

        public CheckinsController(RaceTrackerDataService service)
        {
            dataService = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<List<Checkin>> GetTopCheckins()
        {
            return await dataService.GetTopCheckins(100);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Checkin> GetCheckinById(Guid id)
        {
            return await dataService.GetCheckin(id);
        }

        [HttpGet("unconfirmed")]
        public async Task<List<Checkin>> GetUnconfirmedCheckins()
        {
            return await dataService.GetUnconfirmedCheckins();
        }

        [HttpPost("confirm")]
        public async Task<Checkin> ConfirmCheckin([FromQuery] Guid checkinId, [FromQuery] Guid segmentId)
        {
            return await dataService.ConfirmCheckin(checkinId, segmentId);
        }
    }
}
