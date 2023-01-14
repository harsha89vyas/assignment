using System;
using System.ComponentModel.DataAnnotations;
using Chat.Shared.Entities;

namespace Chat.Entities
{
	public class Room: BaseEntity
	{
        [MaxLength(100)]
        public string Name { get; set; }
        public IEnumerable<Participant>? Participants { get; set; }
        public IEnumerable<Message>? Messages { get; set; }
    }
}

