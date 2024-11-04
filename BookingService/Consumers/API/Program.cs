using Application.Ports;
using Data;
using Microsoft.EntityFrameworkCore;
using API.Middlewares;
using Domain.Guests.Ports;
using Application.Guests;
using Data.Guests;
using Application.Rooms;
using Domain.Rooms.Ports;
using Data.Rooms;
using Data.Pagination;
using Domain.Rooms.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region
builder.Services.AddScoped<PaginationService<Room>>();
builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
#endregion

#region

var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HotelDbContext>(
    options => options.UseSqlServer(connectionString));

#endregion

builder.Services.AddExceptionHandler<ErrorHandlingMiddleware>();
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(typeof(GuestMapping));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();



app.UseExceptionHandler();

app.Run();
