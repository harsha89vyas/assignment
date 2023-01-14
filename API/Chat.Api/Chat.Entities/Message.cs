using System;
using System.ComponentModel.DataAnnotations;
using Chat.Shared.Entities;

namespace Chat.Entities
{
	public class Message : BaseEntity
	{
		[MaxLength(1000)]
		public string Text { get; set; }
		public int ParticipantId { get; set; }
		public int RoomId { get; set; }
		public Room Room { get; set; }
		public Participant Participant { get; set; }
	}
}

