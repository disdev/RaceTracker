using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceTracker.Data.Models
{
    public enum Status
    {
        Registered,
        DNS,
        Started,
        DNF,
        Finished
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class Participant
    {
        public Guid Id { get; set; }
        public string Bib { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Age { get; set; }
        public Gender Gender { get; set; }
        public Guid RaceId { get; set; }
        public Race Race { get; set; }        
        public Status Status { get; set; }
        public List<Checkin> Checkins { get; set; }
        public List<Watcher> Watchers { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public string Hometown
        {
            get
            {
                return $"{City}, {Region}";
            }
        }
    }
}
