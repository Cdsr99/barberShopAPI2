using System.Text;
using BarberShopAPI2.Controllers.Endpoints;
using BarberShopAPI2.Controllers.Endpoints.Schedules.Create;
using BarberShopAPI2.Controllers.Endpoints.Schedules.Delete;
using BarberShopAPI2.Controllers.Endpoints.Booking;
using BarberShopAPI2.Controllers.Endpoints.UserEndPoint;
using BarberShopAPI2.Controllers.Endpoints.UserLoginEndPoint;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using BarberShopAPI2.Profile;
using BarberShopAPI2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Booking = BarberShopAPI2.Models.Booking;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var connString = builder.Configuration
    ["ConnectionStrings:Connection"];

var connStringSymmetric = builder.Configuration
    ["SymmetricSecurityKey"];

builder.Services.
    AddDbContext<BarberShopContext>(option => option
        .UseLazyLoadingProxies().UseSqlServer(connString));


builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BarberShopContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(connStringSymmetric)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(UserProfile));



builder.Services.AddTransient<Dal<Schedule>>();
builder.Services.AddTransient<Dal<Booking>>();
builder.Services.AddTransient<Dal<Settings>>();
builder.Services.AddTransient<Dal<User>>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.AddEndPointSchedulesConsult();
app.AddEndPointSchedulesCreate();
app.AddEndPointSchedulesDelete();
app.AddEndPointBooking();
app.AddEndPointUserCreate();
app.AddEndPointUserLogin();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
