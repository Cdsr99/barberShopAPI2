using BarberShopAPI2.Controllers.Endpoints;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.
    AddDbContext<BarberShopContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddTransient<Dal<Schedule>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddEndPointSchedules();
app.Run();
