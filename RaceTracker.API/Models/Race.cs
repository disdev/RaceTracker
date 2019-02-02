using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceTracker.Data.Models
{
    public enum Unit
    {
        Miles,
        Kilometers
    }

    public class Race
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Distance { get; set; }
        public Unit Unit { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string UltraSignupUrl { get; set; }
        public List<Segment> Segments { get; set; }
        public List<Participant> Participants { get; set; }
        public Guid RaceEventId { get; set; }
        public RaceEvent RaceEvent { get; set; }
    }
}
