using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceTracker.Data.Models
{
    public class Watcher
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public Guid ParticipantId { get; set; }
        public Participant Participant { get; set; }
    }
}
