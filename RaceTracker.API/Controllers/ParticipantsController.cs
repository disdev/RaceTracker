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
    [Route("api/participants")]
    public class ParticipantsController : Controller
    {
        private readonly RaceTrackerDataService dataService;

        public ParticipantsController(RaceTrackerDataService svc)
        {
            dataService = svc;
        }

        // GET: api/participants
        [HttpGet]
        public async Task<List<Leader>> GetParticipants()
        {
            return await dataService.GetLeaders();
        }

        [HttpPost]
        public async Task<Participant> AddOrUpdateParticipant([FromBody] Participant participant)
        {
          return await dataService.AddParticipant(participant);
        }
    }
}