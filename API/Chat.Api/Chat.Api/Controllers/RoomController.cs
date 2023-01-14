using System.Text.RegularExpressions;
using Chat.Api.Dto;
using Chat.Api.Hubs;
using Chat.Api.Hubs.Interfaces;
using Chat.Entities;
using Chat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class RoomController
{
    private readonly IRoomRepository _repository;

    public RoomController(IRoomRepository repository)
	{
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Room>> GetRooms() => await _repository.GetRooms();

    [HttpPost]
    public async Task<int> CreateAsync(Room room)
    {
        room = await _repository.CreateAsync(room);
        return room.Id;
    }

    
    [HttpPost("Join")]
    public async Task<Room> JoinRoomAsync([FromBody]ParticipantDto participantDto)
    {
        //Could use automapper for these conversions
        var participant = new Participant()
        {
            RoomId = participantDto.RoomId,
            Name = participantDto.Name
        };
        var room = await _repository.JoinRoomAsync(participant);
        room.Messages = room.Messages.OrderBy(x => x.CreatedDate);
        participant = room.Participants.FirstOrDefault(x => x.Name.Equals(participant.Name, StringComparison.InvariantCultureIgnoreCase));
        
        return room;
    }

    [HttpGet("Init")]
    public void Initialize()
    {
        _repository.AddTestData();  
    }
}


