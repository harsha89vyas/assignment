using System;
namespace Chat.Repositories
{
	public class ParticipantRepository:BaseRepository
	{
		public ParticipantRepository(ChatDBContext dBContext): base(dBContext)
		{
		}
	}
}

