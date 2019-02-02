using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaceTracker.Data.Models
{
    public class Segment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Race Race { get; set; }
        public Guid RaceId { get; set; }
        public int Order { get; set; }
        public string GeoJson { get; set; }
        public Guid? FromCheckpointId { get; set; }
        public Checkpoint FromCheckpoint { get; set; }
        public Guid? ToCheckpointId { get; set; }
        public Checkpoint ToCheckpoint { get; set; }
        public Double Distance { get; set; }
        public Double TotalDistance { get; set; }
    }
}
