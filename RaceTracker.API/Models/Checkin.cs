using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RaceTracker.Data.Models
{
    public class Checkin
    {
        public Guid Id { get; set; }
        public DateTime When { get; set; }
        public Guid ParticipantId { get; set; }
        public Participant Participant { get; set; }
        public Guid? SegmentId { get; set; }
        public Segment Segment { get; set; }
        public bool Confirmed { get; set; }
        public string Note { get; set; }
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
    }
}
