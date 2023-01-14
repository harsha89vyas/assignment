using Chat.Api.Dto;
using Chat.Api.Hubs;
using Chat.Api.Hubs.Interfaces;
using Chat.Entities;
using Chat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MessageController
{
    private readonly IMessageRepository _repository;

    private readonly IHubContext<RoomHub, IRoomClient> _hub;

    public MessageController(IMessageRepository repository, IHubContext<RoomHub, IRoomClient> hub)
	{
        _repository = repository;
        _hub = hub;
    }   

    /// <summary>
    /// Creating a message and broadcasting to the room.
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Create(MessageDto messageDto)
    {
        var message = new Message()
        {
            Text = messageDto.Text,
            RoomId = messageDto.RoomId,
            ParticipantId = messageDto.ParticipantId
        };
        message = await _repository.CreateAsync(message);
        await _hub.Clients.Group(messageDto.RoomId.ToString()).NewMessageBroadcast(message);
        return message.Id;
    }

}


