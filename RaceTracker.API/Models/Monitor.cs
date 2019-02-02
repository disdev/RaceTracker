using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaceTracker.Data;
using Newtonsoft.Json;

namespace RaceTracker.Data.Models
{
    public class Monitor
    {
        public Guid Id { get; set; }
        public Guid CheckpointId { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
    }
}
