using AutoMapper;
using BarberShopAPI2.Controllers.Request.UsersRequests;
using BarberShopAPI2.Models;
using BarberShopAPI2.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI2.Controllers.Endpoints.UserLoginEndPoint;

public static class UsersLoginController
{
    private static IMapper _mapper;
    private static UserManager<User> _userManager;
    //private static UserService _userService;

    public static void AddEndPointUserLogin(this WebApplication app)
    {
        var userGroup = app.MapGroup("/user");

        #region Authenticate an User

        userGroup.MapPost("/login",
            async ([FromServices] UserService _userService,UserLoginRequest userLoginRequest) =>
            {
                try
                {
                    var token = await _userService.Login(userLoginRequest);
                    return Results.Ok(token);
                }
                catch (Exception error)
                {
                    Console.WriteLine($"This is the error: {error.Message}");
                    return Results.BadRequest(new { Error = error.Message });
                }

            });

        #endregion
    }
}