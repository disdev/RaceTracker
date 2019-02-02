using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RaceTracker.Data.Models;

namespace RaceTracker.Data
{
    public class DbInitializer
    {

        public const string RACE_ID_1 = "35A78E84-CA73-4BF0-9EDA-11710A8BA492";
        public const string RACE_CODE_1 = "100M";
        public const string RACE_ID_2 = "B913837D-08A3-4CF5-936A-3F70B9417A89";
        public const string RACE_CODE_2 = "100K";
        private RaceTrackerDataService service;

        public void Initialize(RaceTrackerContext context)
        {
            SeedDatabase(context);
        }

        public void SeedDatabase(RaceTrackerContext context)
        {
            service = new RaceTrackerDataService(context);
            
            EmptyDatabase(context);
            
            #region Races

            Random rnd = new Random();

            var e1 = new RaceEvent() {
                Id = Guid.NewGuid(),
                Name = "LOVIT",
                Location = "Lake Ouachita",
                Start = new DateTime(2019, 2, 22)
            };
            context.RaceEvents.Add(e1);
            context.SaveChanges();

            var r1 = new Race()
            {
                Id = Guid.Parse(RACE_ID_1),
                Name = "100 Mile",
                Code = RACE_CODE_1,
                Distance = 100.0M,
                Unit = Unit.Miles,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(34),
                UltraSignupUrl = "https://ultrasignup.com/entrants_event.aspx?did=48514",
                RaceEvent = e1,
                Segments = new List<Segment>(),
                Participants = new List<Participant>()
            };

            var r2 = new Race()
            {
                Id = Guid.Parse(RACE_ID_2),
                Name = "100 Kilometer",
                Code = RACE_CODE_2,
                Distance = 100.0M,
                Unit = Unit.Kilometers,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(21),
                UltraSignupUrl = "https://ultrasignup.com/entrants_event.aspx?did=48515",
                RaceEvent = e1,
                Segments = new List<Segment>(),
                Participants = new List<Participant>()
            };

            context.Races.AddRange(new Race[] { r1, r2 });

            #endregion

            #region Checkpoints
            var c1 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "East Cove Pavilion",
                GeoJson = @"",
                Number = 0
            };

            var c2 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Hickory Nut Mountain",
                GeoJson = @"",
                Number = 1
            };

            var c3 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Joplin Road Crossing",
                GeoJson = @"",
                Number = 2
            };

            var c4 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Tompkins Bend",
                GeoJson = @"",
                Number = 3
            };

            var c5 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "ADA",
                GeoJson = @"",
                Number = 4
            };

            var c6 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Forest Road 47A",
                GeoJson = @"",
                Number = 5
            };

            var c7 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Charlton",
                GeoJson = @"",
                Number = 6
            };

            var c8 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Crystal Springs",
                GeoJson = @"",
                Number = 7
            };

            var c9 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Bear Creek",
                GeoJson = @"",
                Number = 8
            };

            var c10 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Brady Mountain Road",
                GeoJson = @"",
                Number = 9
            };

            var c11 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Spillway ",
                GeoJson = @"",
                Number = 10
            };

            var c12 = new Checkpoint()
            {
                Id = Guid.NewGuid(),
                Name = "Avery Rec Area",
                GeoJson = @"",
                Number = 11
            };

            context.Checkpoints.AddRange(new Checkpoint[] {
                c1, c2, c3, c4, c5, c6, c7, c8, c9, c10,
                c11, c12
            });

            #endregion

            #region Segments

            var s1 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Start to Hickory Nut Mountain",
                GeoJson = @"segment-1.json",
                FromCheckpoint = c1,
                ToCheckpoint = c2,
                Order = 1,
                Distance = 4,
                TotalDistance = 4
            };

            var s2 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Hickory Nut Mountain to Joplin Road Crossing",
                GeoJson = @"segment-2.json",
                FromCheckpoint = c2,
                ToCheckpoint = c3,
                Order = 2,
                Distance = 5,
                TotalDistance = 9
            };

            var s3 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Joplin Road Crossing to Tompkins Bend",
                GeoJson = @"segment-3.json",
                FromCheckpoint = c3,
                ToCheckpoint = c4,
                Order = 3,
                Distance = 4,
                TotalDistance = 13
            };

            var s4 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Tompkins Bend to ADA",
                GeoJson = @"segment-4.json",
                FromCheckpoint = c4,
                ToCheckpoint = c5,
                Order = 4,
                Distance = 6,
                TotalDistance = 19
            };

            var s5 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "ADA to Tompkins Bend",
                GeoJson = @"segment-5.json",
                FromCheckpoint = c5,
                ToCheckpoint = c4,
                Order = 5,
                Distance = 6,
                TotalDistance = 25
            };

            var s6 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Tompkins Bend to Joplin Road Crossing",
                GeoJson = @"segment-6.json",
                FromCheckpoint = c4,
                ToCheckpoint = c3,
                Order = 6,
                Distance = 5,
                TotalDistance = 30
            };

            var s7 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Joplin Road Crossing to Hickory Nut Mountain",
                GeoJson = @"segment-7.json",
                FromCheckpoint = c3,
                ToCheckpoint = c2,
                Order = 7,
                Distance = 4,
                TotalDistance = 34
            };

            var s8 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Hickory Nut Mountain to Forest Road 47A",
                GeoJson = @"segment-8.json",
                FromCheckpoint = c2,
                ToCheckpoint = c6,
                Order = 8,
                Distance = 4,
                TotalDistance = 38
            };

            var s9 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Forest Road 47A to Charlton",
                GeoJson = @"segment-9.json",
                FromCheckpoint = c6,
                ToCheckpoint = c7,
                Order = 9,
                Distance = 4,
                TotalDistance = 42
            };

            var s10 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Charlton to Crystal Springs",
                GeoJson = @"segment-10.json",
                FromCheckpoint = c7,
                ToCheckpoint = c8,
                Order = 10,
                Distance = 5,
                TotalDistance = 47
            };

            var s11 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Crystal Springs to Bear Creek",
                GeoJson = @"segment-11.json",
                FromCheckpoint = c8,
                ToCheckpoint = c9,
                Order = 11,
                Distance = 3,
                TotalDistance = 50
            };

            var s12 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Bear Creek to Brady Mountain Road",
                GeoJson = @"segment-12.json",
                FromCheckpoint = c9,
                ToCheckpoint = c10,
                Order = 12,
                Distance = 8,
                TotalDistance = 58
            };

            var s13 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Brady Mountain Road to Spillway",
                GeoJson = @"segment-13.json",
                FromCheckpoint = c10,
                ToCheckpoint = c11,
                Order = 13,
                Distance = 4,
                TotalDistance = 62
            };

            var s14 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Spillway to Avery Rec Area",
                GeoJson = @"segment-14.json",
                FromCheckpoint = c11,
                ToCheckpoint = c12,
                Order = 14,
                Distance = 3,
                TotalDistance = 65
            };

            var s15 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Avery Rec Area to Spillway",
                GeoJson = @"segment-15.json",
                FromCheckpoint = c12,
                ToCheckpoint = c11,
                Order = 15,
                Distance = 3,
                TotalDistance = 68
            };

            var s16 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Spillway to Brady Mountain Road",
                GeoJson = @"segment-16.json",
                FromCheckpoint = c11,
                ToCheckpoint = c10,
                Order = 16,
                Distance = 4,
                TotalDistance = 72
            };

            var s17 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Brady Mountain Road to Bear Creek",
                GeoJson = @"segment-17.json",
                FromCheckpoint = c10,
                ToCheckpoint = c9,
                Order = 17,
                Distance = 8,
                TotalDistance = 80
            };

            var s18 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Bear Creek to Crystal Springs",
                GeoJson = @"segment-18.json",
                FromCheckpoint = c9,
                ToCheckpoint = c8,
                Order = 18,
                Distance = 3,
                TotalDistance = 83
            };

            var s19 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Crystal Springs to Charlton",
                GeoJson = @"segment-19.json",
                FromCheckpoint = c8,
                ToCheckpoint = c7,
                Order = 19,
                Distance = 4,
                TotalDistance = 87
            };

            var s20 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Charlton to Forest Road 47A",
                GeoJson = @"segment-20.json",
                FromCheckpoint = c7,
                ToCheckpoint = c6,
                Order = 20,
                Distance = 5,
                TotalDistance = 92
            };

            var s21 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Forest Road 47A to Hickory Nut Mountain",
                GeoJson = @"segment-21.json",
                FromCheckpoint = c6,
                ToCheckpoint = c2,
                Order = 21,
                Distance = 4,
                TotalDistance = 96
            };

            var s22 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Hickory Nut Mountain to Finish",
                GeoJson = @"segment-22.json",
                FromCheckpoint = c2,
                ToCheckpoint = c1,
                Order = 22,
                Distance = 4,
                TotalDistance = 100
            };



            var s23 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Start to Hickory Nut Mountain",
                GeoJson = @"segment-1.json",
                FromCheckpoint = c1,
                ToCheckpoint = c2,
                Order = 1,
                Distance = 4,
                TotalDistance = 4
            };
            var s24 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Hickory Nut Mountain to Forest Road 47A",
                GeoJson = @"",
                FromCheckpoint = c2,
                ToCheckpoint = c6,
                Order = 2,
                Distance = 3.5,
                TotalDistance = 7.5
            };

            var s25 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Forest Road 47A to Crystal Springs",
                GeoJson = @"",
                FromCheckpoint = c6,
                ToCheckpoint = c8,
                Order = 3,
                Distance = 4.5,
                TotalDistance = 12
            };

            var s26 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Crystal Springs to Bear Creek",
                GeoJson = @"",
                FromCheckpoint = c8,
                ToCheckpoint = c9,
                Order = 4,
                Distance = 3.5,
                TotalDistance = 15.5
            };

            var s27 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Bear Creek to Brady Mountain Road",
                GeoJson = @"",
                FromCheckpoint = c9,
                ToCheckpoint = c10,
                Order = 5,
                Distance = 7.5,
                TotalDistance = 23
            };

            var s28 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Brady Mountain Road to Spillway",
                GeoJson = @"",
                FromCheckpoint = c10,
                ToCheckpoint = c11,
                Order = 6,
                Distance = 4,
                TotalDistance = 27
            };

            var s29 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Spillway to Avery Rec Area",
                GeoJson = @"",
                FromCheckpoint = c11,
                ToCheckpoint = c12,
                Order = 7,
                Distance = 3,
                TotalDistance = 30
            };

            var s30 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Avery Rec Area to Spillway",
                GeoJson = @"",
                FromCheckpoint = c12,
                ToCheckpoint = c11,
                Order = 8,
                Distance = 3,
                TotalDistance = 33
            };

            var s31 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Spillway to Brady Mountain Road",
                GeoJson = @"",
                FromCheckpoint = c11,
                ToCheckpoint = c10,
                Order = 9,
                Distance = 3,
                TotalDistance = 37
            };

            var s32 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Brady Mountain Road to Bear Creek",
                GeoJson = @"",
                FromCheckpoint = c10,
                ToCheckpoint = c9,
                Order = 10,
                Distance = 7.5,
                TotalDistance = 44.5
            };

            var s33 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Bear Creek to Crystal Springs",
                GeoJson = @"",
                FromCheckpoint = c9,
                ToCheckpoint = c8,
                Order = 11,
                Distance = 3.5,
                TotalDistance = 48
            };

            var s34 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Crystal Springs to Forest Road 47A",
                GeoJson = @"",
                FromCheckpoint = c8,
                ToCheckpoint = c6,
                Order = 12,
                Distance = 4.5,
                TotalDistance = 52.5
            };

            var s35 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Forest Road 47A to Hickory Nut Mountain",
                GeoJson = @"",
                FromCheckpoint = c6,
                ToCheckpoint = c2,
                Order = 13,
                Distance = 3.5,
                TotalDistance = 56
            };

            var s36 = new Segment()
            {
                Id = Guid.NewGuid(),
                Name = "Hickory Nut Mountain to Finish",
                GeoJson = @"",
                FromCheckpoint = c2,
                ToCheckpoint = c1,
                Order = 14,
                Distance = 4,
                TotalDistance = 60
            };

            var r1Segments = new List<Segment>()
            {
                s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22
            };
            r1.Segments = r1Segments;

            var r2Segments = new List<Segment>()
            {
                s23, s24, s25, s26, s27, s28, s29, s30, s31, s32, s33, s34, s35, s36
            };
            r2.Segments = r2Segments;

            context.SaveChanges();

            #endregion

            #region Monitors

            foreach (var checkpoint in context.Checkpoints)
            {
                var m = new Monitor()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Monitor",
                    PhoneNumber = "+15015551212",
                    Checkpoint = checkpoint
                };

                context.Monitors.Add(m);
            }

            context.SaveChanges();

            #endregion
            
            GenerateParticipants(10, 1, r1, true, context);
            GenerateParticipants(10, 101, r2, true, context);
            
            context.SaveChanges();
        }

        public void EmptyDatabase(RaceTrackerContext context)
        {
            context.Participants.RemoveRange(context.Participants);
            context.Checkpoints.RemoveRange(context.Checkpoints);
            context.Segments.RemoveRange(context.Segments);
            context.Monitors.RemoveRange(context.Monitors);
            context.Checkins.RemoveRange(context.Checkins);
            context.Races.RemoveRange(context.Races);
            context.RaceEvents.RemoveRange(context.RaceEvents);
            context.SaveChanges();
        }

        private void GenerateParticipants(int count, int startingBib, Race race, bool addProgress, RaceTrackerContext context)
        {
            Random rnd = new Random();
            
            for (int i = 1; i <= count; i++)
            {
                var quality = rnd.Next(1, 5);

                var p = new Participant()
                {
                    Id = Guid.NewGuid(),
                    Bib = (startingBib + i).ToString(),
                    FirstName = "Participant",
                    LastName = (startingBib + i).ToString(),
                    City = "Charleston",
                    Region = "SC",
                    Age = $"{rnd.Next(18, 70)}",
                    Gender = (Gender)rnd.Next(1, 2),
                    Status = Status.Registered,
                    Checkins = new List<Checkin>(),
                    RaceId = race.Id
                };
                //context.Participants.Add(p);
                //context.SaveChanges();

                p = service.AddParticipant(p).Result;
                                
                DateTime lastCheckin = DateTime.Now;

                /*
                foreach (var segment in race.Segments.OrderBy(x => x.Order))
                {
                    var checkin = service.AddCheckin(p.Id, "+15015551212", CalculateWhen(segment.Distance, quality, lastCheckin));
                    service.ConfirmCheckin(checkin.Id);
                    lastCheckin = checkin.When;
                }
                */
            }
        }

        private DateTime CalculateWhen(double distance, int quality, DateTime last)
        {
            Random rnd = new Random();

            int mins = rnd.Next(8 + quality, 10 + quality);
            int secs = rnd.Next(0, 59);

            int pace = mins * 60 + secs;
            double elapsed = pace * distance;

            return last.AddSeconds(elapsed);
        }
    }
}