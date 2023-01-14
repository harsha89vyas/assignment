using System;
using Chat.Entities;

namespace Chat.Repositories.Interfaces
{
	public interface IMessageRepository
	{
		IEnumerable<Message> GetMessages(int roomId);
        Task<Message> CreateAsync(Message message);
    }
}

