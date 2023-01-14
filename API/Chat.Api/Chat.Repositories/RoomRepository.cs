using System;
using Chat.Entities;
using Chat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repositories
{
	public class RoomRepository: BaseRepository, IRoomRepository
    {
		public RoomRepository(ChatDBContext dBContext) : base(dBContext)
		{
		}

        public async Task<Room> CreateAsync(Room room)
        {
            DbContext.Rooms.Add(room);
            await DbContext.SaveChangesAsync();
            return room;
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await DbContext.Rooms.ToListAsync();
        }

        public void AddTestData()
        {
            var testRooms = new List<Chat.Entities.Room>
            {
                new Chat.Entities.Room()
                {
                    Name = "Science"
                },
                new Chat.Entities.Room()
                {
                    Name = "Biology"
                },
                new Chat.Entities.Room()
                {
                    Name = "Physics"
                }
            };
            var testParticipants = new List<Chat.Entities.Participant>
            {
                new Chat.Entities.Participant()
                {
                    Name = "Alice",
                    RoomId = 1
                },
                new Chat.Entities.Participant()
                {
                    Name = "Bob",
                    RoomId = 1
                },
                new Chat.Entities.Participant()
                {
                    Name = "Mary",
                    RoomId = 1
                },
                new Chat.Entities.Participant()
                {
                    Name = "Alice",
                    RoomId = 2
                },
                new Chat.Entities.Participant()
                {
                    Name = "Bob",
                    RoomId = 2
                },
                new Chat.Entities.Participant()
                {
                    Name = "Mary",
                    RoomId = 2
                }
            };

            DbContext.Rooms.AddRange(testRooms);
            DbContext.Participants.AddRange(testParticipants);


            DbContext.SaveChanges();
        }

        public async Task<Room> JoinRoomAsync(Participant participant)
        {
            var room = await DbContext.Rooms.AsNoTracking().Include(x => x.Participants)
                                          .Include(x => x.Messages)
                                          .FirstOrDefaultAsync(x=>x.Id == participant.RoomId);
            if(room.Participants?.Any(x=>x.Name.Equals(participant.Name, StringComparison.InvariantCultureIgnoreCase))??false == false)
            {
                DbContext.Participants.Add(participant);
                await DbContext.SaveChangesAsync();
                room.Participants = room.Participants ?? new List<Participant>();
                room.Participants.Append(participant);
            }
            return room;
        }
    }
}

