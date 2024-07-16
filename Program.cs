using System.Text;
using BarberShopAPI2.Controllers.Endpoints;
using BarberShopAPI2.Controllers.Endpoints.Schedules.Create;
using BarberShopAPI2.Controllers.Endpoints.Schedules.Delete;
using BarberShopAPI2.Controllers.Endpoints.Booking;
using BarberShopAPI2.Controllers.Endpoints.UserEndPoint;
using BarberShopAPI2.Controllers.Endpoints.UserEndPoint.Consult;
using BarberShopAPI2.Controllers.Endpoints.UserLoginEndPoint;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using BarberShopAPI2.Profile;
using BarberShopAPI2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

/*
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
*/

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddTransient<Dal<Schedule>>();
builder.Services.AddTransient<Dal<Booking>>();
builder.Services.AddTransient<Dal<Settings>>();
builder.Services.AddTransient<Dal<User>>();

//builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<TokenService>();
//builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin() // Adicione aqui a origem da sua aplicação Blazor
               .AllowAnyMethod()
               .AllowAnyHeader();
               
    });
}); 




var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();

#region User

app.AddEndPointUserConsult();
app.AddEndPointUserLogin();
app.AddEndPointUserCreate();
app.AddEnPointUserUpdate();

#endregion

#region Schedule

app.AddEndPointSchedulesConsult();
app.AddEndPointSchedulesCreate();
app.AddEndPointSchedulesDelete();

#endregion

#region Booking

app.AddEndPointBookingConsult();
app.AddEndPointBookingCreate();
app.AddEndPointBookingUpdate();
app.AddEndPointBookingDelete();

#endregion


//app.UseAuthentication();
//app.UseAuthorization();
app.UseCors("CorsPolicy");

app.Run();
