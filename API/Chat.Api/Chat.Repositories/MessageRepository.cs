using Chat.Entities;
using Chat.Repositories.Interfaces;

namespace Chat.Repositories
{
    public class MessageRepository: BaseRepository,IMessageRepository
    {

        public MessageRepository(ChatDBContext chatDBContext): base(chatDBContext)
		{
        }

        public async Task<Message> CreateAsync(Message message)
        {
            DbContext.Messages.Add(message);
            await DbContext.SaveChangesAsync();
            return message;
        }

        public IEnumerable<Message> GetMessages(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}

