using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceTracker.Data.Models
{
    public class Checkpoint
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string GeoJson { get; set; }
        public int Number { get; set; }
        public List<Monitor> Monitors { get; set; }
    }
}
