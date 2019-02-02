using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RaceTracker.Data.Models
{
    public class Leader
    {
        public Guid Id { get; set; }
        public Guid ParticipantId { get; set; }
        public Participant Participant { get; set; }
        public Status Status { get; set; }
        public int Progress { get; set; }
        public Checkin LastCheckin { get; set; }
        public double ElapsedTime { get; set; }
        public Dictionary<int, DateTime> Checkins { get; set; } = new Dictionary<int, DateTime>();
    }
}
