using System;
using Chat.Entities;

namespace Chat.Repositories.Interfaces
{
	public interface IRoomRepository
	{
		Task<Room> CreateAsync(Room room);
		Task<IEnumerable<Room>> GetRooms();
		void AddTestData();
        Task<Room> JoinRoomAsync(Participant participant);
    }
}

