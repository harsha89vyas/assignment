using System;
using Chat.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Api.Dto
{
	public class ParticipantDto
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public int RoomId { get; set; }
    }
}

