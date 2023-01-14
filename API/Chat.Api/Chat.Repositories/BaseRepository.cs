using System;
using Chat.Shared.EF.Interfaces;

namespace Chat.Repositories
{
	public class BaseRepository : IBaseRepository
	{
		public BaseRepository(ChatDBContext dbContext)
		{
            DbContext = dbContext;
        }

        public ChatDBContext DbContext { get; }
    }
}

