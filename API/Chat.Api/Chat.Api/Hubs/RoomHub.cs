using Chat.Entities;
using Microsoft.AspNetCore.SignalR;
using Chat.Api.Hubs.Interfaces;

namespace Chat.Api.Hubs
{
    public class RoomHub : Hub<IRoomClient>
	{
		public RoomHub()
		{
		}

        /// <summary>
        /// Notify the group when a new user joins
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        public async Task JoinRoomAsync(Participant participant)
        {
            var groupName = participant.RoomId.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            if (participant != null)
            {
                await Clients.GroupExcept(groupName, Context.ConnectionId).NotifyNewParticipantJoined(participant);
            }
        }

        
    }
}

