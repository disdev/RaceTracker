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
    [Route("api/Leaders")]
    public class LeadersController : Controller
    {
        private readonly RaceTrackerDataService dataService;

        public LeadersController(RaceTrackerDataService svc)
        {
            dataService = svc;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<List<Leader>> GetLeaders()
        {
            return await dataService.GetLeaders();
        }
    }
}