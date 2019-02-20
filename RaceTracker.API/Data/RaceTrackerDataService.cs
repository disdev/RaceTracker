using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RaceTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Lookups.V1;
using Twilio.TwiML;
using Twilio.Types;

namespace RaceTracker.Data
{
    public class RaceTrackerDataService
    {
        private const int START_CHECKPOINT = 0;

        private RaceTrackerContext Db;

        private string TwilioAccountSid = "";
        private string TwilioAuthToken = "";
        private string TwilioAdminPhone = "";
        
        public RaceTrackerDataService(RaceTrackerContext context, IOptions<TwilioSettings> settings)
        {
            Db = context;
            TwilioAccountSid = settings.Value.AccountSid;
            TwilioAuthToken = settings.Value.AuthToken;
            TwilioAdminPhone = settings.Value.AdminPhone;
        }

        public RaceTrackerDataService(RaceTrackerContext context)
        {
            Db = context;
        }
        
        public async Task<RaceEvent> AddRaceEvent(RaceEvent raceEvent)
        {
            raceEvent.Id = Guid.NewGuid();
            Db.RaceEvents.Add(raceEvent);
            await Db.SaveChangesAsync();
            return raceEvent;
        }

        public async Task<List<RaceEvent>> GetRaceEvents()
        {
            return await Db.RaceEvents.ToListAsync();
        }

        public async Task<RaceEvent> GetRaceEvent(Guid raceEventId)
        {
            return await Db.RaceEvents.SingleAsync(raceEvent => raceEvent.Id == raceEventId);
        }

        public async Task<Race> AddRace(Race race) 
        {
            race.Id = Guid.NewGuid();
            Db.Races.Add(race);
            await Db.SaveChangesAsync();
            return race;
        }

        public async Task<List<Race>> GetRaces()
        {
            return await Db.Races.ToListAsync();
        }
        
        public async Task<Race> GetRace(Guid raceId)
        {
            return await Db.Races
                .Where(x => x.Id == raceId)
                .Include(x => x.Participants)
                    .ThenInclude(x => x.Checkins)
                .FirstAsync();
        }

        public async Task<List<Segment>> GetSegments()
        {
            return await Db.Segments
                .Include(x => x.ToCheckpoint)
                .Include(x => x.FromCheckpoint)
                .OrderBy(x => x.Order).ToListAsync();
        }

        public async Task<Segment> AddSegment(Segment segment) 
        {
            segment.Id = Guid.NewGuid();
            Db.Segments.Add(segment);
            await Db.SaveChangesAsync();
            return segment;
        }

        public async Task<Checkpoint> AddCheckpoint(Checkpoint checkpoint) 
        {
            checkpoint.Id = Guid.NewGuid();
            Db.Checkpoints.Add(checkpoint);
            await Db.SaveChangesAsync();
            return checkpoint;
        }

        public async Task<Participant> AddParticipant(Participant participant) 
        {
            var exists = Db.Participants.Any(p => p.FirstName == participant.FirstName && p.LastName == participant.LastName);
            
            if (exists) {
                throw new Exception();
            }
            
            participant.Id = Guid.NewGuid();
            await Db.Participants.AddAsync(participant);

            await Db.Leaders.AddAsync(new Leader() {
                Id = Guid.NewGuid(),
                ParticipantId = participant.Id,
                Status = Status.Registered,
                Progress = 0,
                ElapsedTime = 0
            });

            await Db.SaveChangesAsync();
            return participant;
        }

        public async Task<Monitor> AddMonitor(Monitor monitor) 
        {
            monitor.Id = Guid.NewGuid();
            Db.Monitors.Add(monitor);
            await Db.SaveChangesAsync();
            return monitor;
        }

        public async Task<Monitor> AddMonitor(string phoneNumber, int checkpointNumber)
        {
            var checkpoint = await Db.Checkpoints.SingleAsync(x => x.Number == checkpointNumber);
            var monitor = await Db.Monitors.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber && x.Checkpoint == checkpoint);

            if (monitor == null)
            {
                monitor = new Monitor()
                {
                    Id = Guid.NewGuid(),
                    Name = "",
                    PhoneNumber = phoneNumber,
                    Active = true,
                    Checkpoint = checkpoint
                };
                
                await Db.Monitors.AddAsync(monitor);
                await Db.SaveChangesAsync();
            }

            return monitor;
        }

        public async Task<Message> AddMessage(Message message)
        {
            message.Id = Guid.NewGuid();
            Db.Messages.Add(message);
            await Db.SaveChangesAsync();
            return message;
        }

        public async Task<List<Checkin>> GetUnconfirmedCheckins()
        {
            return await Db.Checkins.Where(x => x.Confirmed == false)
                .Include(x => x.Participant)
                .Include(x => x.Message)
                .Include(x => x.Segment)
                .ToListAsync();
        }

        public async Task<List<Checkin>> GetCheckins()
        {
            return await Db.Checkins
                .Include(x => x.Participant)
                .Include(x => x.Message)
                .Include(x => x.Segment)
                .ToListAsync();
        }

        public async Task<Checkin> GetCheckin(Guid id)
        {
            return await Db.Checkins
                .Include(x => x.Participant)
                .Include(x => x.Message)
                .Include(x => x.Segment)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<List<Checkin>> GetTopCheckins(int limit)
        {
            return await Db.Checkins
                .Include(x => x.Participant)
                .Include(x => x.Message)
                .Include(x => x.Segment)
                .OrderByDescending(x => x.When)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<Leader>> GetLeaders()
        {
            return await Db.Leaders
                .Include(x => x.Participant)
                .Include(x => x.LastCheckin)
                    .ThenInclude(x => x.Segment)
                .OrderBy(x => x.LastCheckin.When)
                .OrderByDescending(x => x.Progress)
                .ToListAsync();
        }

        public async Task<string> HandleMessage(Message message) 
        {
            // Split the message body into parts
            var messageParts = message.Body.Trim().Split(' ');

            if (messageParts[0].Trim().ToUpper() == "START")
            {
                // We're starting the race!
                var race = await this.StartRace(message.From, messageParts[1].Trim(), message.Received);
                return $"Started {race.Code}.";
            }
            else if (messageParts[0].Trim().ToUpper() == "SETUP")
            {
                // We're setting up a new monitor
                var monitor = await this.AddMonitor(message.From, Convert.ToInt16(messageParts[1].Trim()));
                return $"You're set up as a monitor for {monitor.Checkpoint.Name}.";
            }
            else
            {
                var checkins = await this.AddCheckins(message);
                return $"Checked in {checkins.Count} runner{(checkins.Count > 1 ? "s" : "")}.";
            }
        }

        public async Task<List<Checkin>> AddCheckins(Message message)
        {
            var checkins = new List<Checkin>();

            // We're checking in one or more runners
            foreach (var part in message.Body.Split(' '))
            {
                try
                {
                    checkins.Add(await this.AddCheckin(part, message));
                }
                catch (Exception ex)
                {
                    SendSms(TwilioAdminPhone, $"Checkin error! Bib {part}, from {message.From}. Msg: {message.Id}");
                }
                
            }

            return checkins;
        }

        public async Task<Checkin> AddCheckin(string number, Message message)
        {
            var participant = await Db.Participants.Where(x => x.Bib == number).Include(x => x.Race).Include(x => x.Checkins).ThenInclude(x => x.Segment).SingleAsync();
            var monitors = await Db.Monitors.Where(x => x.PhoneNumber == message.From).Include(x => x.Checkpoint).ToListAsync();
            
            if (participant.Status == Status.Started)
            {
                // Where do we think the participant should be?
                var checkinCount = participant.Checkins.Count;
                var expectedSegment = new Segment();
                if (checkinCount > 0)
                {
                    var lastSegment = participant.Checkins.OrderByDescending(x => x.When).First().Segment;
                    expectedSegment = await Db.Segments.Where(x => x.Order == (lastSegment.Order + 1) && x.RaceId == participant.RaceId).FirstAsync();
                }
                else
                {
                    expectedSegment = await Db.Segments.Where(x => x.Order == 1 && x.RaceId == participant.RaceId).FirstAsync();
                }
                
                var checkin = new Checkin()
                {
                    Id = Guid.NewGuid(),
                    Participant = participant,
                    Segment = expectedSegment,
                    When = message.Received,
                    Confirmed = monitors.Any(x => x.Checkpoint == expectedSegment.ToCheckpoint), // Does the monitor's checkpoints match where the participant should be?
                    Message = message
                };

                Db.Checkins.Add(checkin);
                await Db.SaveChangesAsync();
                
                if (checkin.Confirmed)
                {
                    await ConfirmCheckin(checkin, expectedSegment);
                }

                return checkin;
            }
            else
            {
                return null;
            }
        }

        private async Task<Participant> FinishRace(Participant participant, Checkin checkin)
        {
            var finish = await Db.Checkpoints.Where(x => x.Number == 0).FirstAsync();

            if (checkin.Segment.ToCheckpointId == finish.Id)
            {
                participant.Status = Status.Finished;
                await Db.SaveChangesAsync();
            }

            return participant;
        }

        public async Task<Leader> UpdateLeader(Checkin checkin)
        {
            // now update the leaderboard
            var leader = await Db.Leaders.SingleOrDefaultAsync(x => x.ParticipantId == checkin.ParticipantId);
            leader.ParticipantId = checkin.ParticipantId;
            leader.Status = Status.Started;
            leader.Progress = checkin.Segment.Order;
            leader.LastCheckin = checkin;
            leader.ElapsedTime = 0;
            leader.Checkins.Add(checkin.Segment.Order, checkin.When);
            
            Db.Leaders.Update(leader);

            await Db.SaveChangesAsync();

            return leader;
        }

        public void DeleteCheckin(Guid checkinId)
        {
            Db.Checkins.Remove(Db.Checkins.Where(x => x.Id == checkinId).First());
            Db.SaveChanges();
        }

        public async Task<Checkin> ConfirmCheckin(Guid checkinId, Guid segmentId)
        {
            return await ConfirmCheckin(
                await Db.Checkins.Include(x => x.Participant).FirstAsync(x => x.Id == checkinId), 
                await Db.Segments.FirstAsync(x => x.Id == segmentId));
        }
        public async Task<Checkin> ConfirmCheckin(Checkin checkin, Segment segment)
        {
            checkin.SegmentId = segment.Id;
            checkin.Confirmed = true;

            Db.Checkins.Update(checkin);
            await Db.SaveChangesAsync();

            await UpdateLeader(checkin);

            // Is this the finish?
            if (checkin.Segment.Id == Db.Segments.Where(x => x.RaceId == checkin.Participant.RaceId).OrderByDescending(x => x.Order).First().Id)
            {
                checkin.Participant.Status = Status.Finished;
            }

            await Db.SaveChangesAsync();

            NotifyWatchers(checkin);

            return checkin;
        }

        public Watcher AddWatcher(string number, Guid participantId)
        {
            var validatedNumber = CheckPhoneNumber(number);
            var participant = Db.Participants.Where(x => x.Id == participantId).First();

            var watcher = new Watcher()
            {
                Id = Guid.NewGuid(),
                PhoneNumber = validatedNumber,
                Participant = participant
            };

            Db.Watchers.Add(watcher);
            Db.SaveChanges();

            SendSms(validatedNumber, $"You're signed up to receive LOViT updates for {participant.FullName}. Reply STOP to end.");

            return watcher;
        }

        public void NotifyWatchers(Checkin checkin)
        {
            var participant = Db.Participants.Where(x => x.Id == checkin.Participant.Id).Include(x => x.Watchers).First();
            var segment = Db.Segments.Where(x => x.Id == checkin.Segment.Id).Include(x => x.ToCheckpoint).First();

            foreach (var watcher in participant.Watchers)
            {
                SendSms(watcher.PhoneNumber, $"{participant.FullName} checked into {segment.ToCheckpoint.Name} at {segment.TotalDistance} miles.");
            }
        }

        public async Task<Race> StartRace(string from, string raceCode, DateTime when)
        {
            var valid = await Db.Monitors.AnyAsync(x => x.PhoneNumber == from && x.Checkpoint.Number == START_CHECKPOINT);

            if (valid)
            {
                var race = await Db.Races.FirstOrDefaultAsync(x => x.Code == raceCode);
                
                race.Start = when;

                await Db.Participants.Where(x => x.RaceId == race.Id).ForEachAsync(participant => {
                    participant.Status = Status.Started;
                });

                await Db.Leaders.Where(x => x.Participant.RaceId == race.Id).ForEachAsync(leader => {
                    leader.Status = Status.Started;
                    leader.Progress = 0;
                    leader.ElapsedTime = 0;
                });

                Db.SaveChanges();

                return race;
            }
            else
            {
                throw new Exception("Something went wrong starting the race!");
            }            
        }

        private void SendSms(string to, string body)
        {
            TwilioClient.Init(TwilioAccountSid, TwilioAuthToken);

            var sendTo = new PhoneNumber(to);
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+15014062030"),
                body: body);
        }

        public async Task<Watcher> Subscribe(string bib, string phoneNumber)
        {
            var phone = CheckPhoneNumber(phoneNumber);
            var participant = await Db.Participants.SingleAsync(x => x.Bib == bib);
            var exists = await Db.Watchers.AnyAsync(x => x.ParticipantId == participant.Id && x.PhoneNumber == phone);

            if (!exists) 
            {
                var watcher = new Watcher()
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = phone,
                    ParticipantId = participant.Id
                };

                await Db.Watchers.AddAsync(watcher);
                await Db.SaveChangesAsync();

                SendSms(phone, $"Subscribed to race updates for {participant.FullName}. Reply STOP to end.");

                return watcher;
            }
            else
            {
                SendSms(phone, $"Subscribed to race updates for {participant.FullName}. Reply STOP to end.");
                return await Db.Watchers.FirstAsync(x => x.ParticipantId == participant.Id && x.PhoneNumber == phone);
            }
        }
        public string CheckPhoneNumber(string inputNumber)
        {
            TwilioClient.Init(TwilioAccountSid, TwilioAuthToken);

            var phoneNumber = PhoneNumberResource.Fetch(
                new PhoneNumber(inputNumber),
                type: new List<string> { "carrier" });

            return phoneNumber.PhoneNumber.ToString();
        }
    }
}
