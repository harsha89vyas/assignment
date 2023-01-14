using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chat.Shared.Entities;

namespace Chat.Entities
{
	public class Participant: BaseEntity
	{
		[MaxLength(100)]
		public string Name { get; set; }
		
		public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
	}
}

