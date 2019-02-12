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
    [Route("api/Segments")]
    public class SegmentsController : Controller
    {
        private readonly RaceTrackerDataService dataService;

        public SegmentsController(RaceTrackerDataService svc)
        {
            dataService = svc;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<List<Segment>> GetLeaders()
        {
            return await dataService.GetSegments();
        }
    }
}