using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaceTracker.Data;
using RaceTracker.Data.Models;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace RaceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : Controller
    {
        private readonly RaceTrackerDataService dataService;

        public MessagesController(RaceTrackerDataService service)
        {
            dataService = service;
        }

        [HttpPost]
        public async Task<TwiMLResult> AddMessage([FromForm] SmsRequest incomingSms)
        {
            var message = new Message() {
                Id = Guid.NewGuid(),
                From = incomingSms.From,
                Body = incomingSms.Body.Trim(),
                FromCity = incomingSms.FromCity,
                FromState = incomingSms.FromState,
                FromZip = incomingSms.FromZip,
                FromCountry = incomingSms.FromCountry,
                Received = DateTime.UtcNow
            };

            message = await dataService.AddMessage(message);

            var responseString = await dataService.HandleMessage(message);

            var response = new MessagingResponse();
            response.Message(responseString);
            return new TwiMLResult(responseString);
        }
    }
}
