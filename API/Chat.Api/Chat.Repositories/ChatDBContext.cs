using System;
using Chat.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repositories
{
	public class ChatDBContext: DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase(databaseName: "ChatDb");
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}

