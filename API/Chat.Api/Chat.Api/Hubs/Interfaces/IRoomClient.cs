using System;
using Chat.Entities;

namespace Chat.Api.Hubs.Interfaces
{
	public interface IRoomClient
	{
		Task NotifyNewParticipantJoined(Participant participant);
		Task NewMessageBroadcast(Message message);
	}
}

