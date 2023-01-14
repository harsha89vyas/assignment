using System;
using System.Xml.Linq;
using Chat.Api.Hubs;
using Chat.Repositories;
using Chat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ChatDBContext>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSignalR();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => {
        x.WithOrigins("http://localhost:4200", "http://localhost:5235");
        x.AllowCredentials();
        x.AllowAnyMethod();
        x.AllowAnyHeader();
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<RoomHub>("/signalr/room");
app.Run();



