using System;
using Chat.Entities;

namespace Chat.Repositories.Interfaces
{
	public interface IParticipantRepository
	{
		Participant Create(Participant participant);
		IEnumerable<Participant> GetParticipants(int roomId);
	}
}

