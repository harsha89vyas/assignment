using System;
namespace Chat.Api.Dto
{
	public class MessageDto
	{
		public string Text { get; set; }
		public int RoomId { get; set; }
		public int ParticipantId { get; set; }
	}
}

