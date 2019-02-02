using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceTracker.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string FromCity { get; set; }
        public string FromState { get; set; }
        public string FromCountry { get; set; }
        public string FromZip { get; set; }
        public DateTime Received { get; set; }
    }
}
