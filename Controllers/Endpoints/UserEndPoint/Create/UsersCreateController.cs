using AutoMapper;
using BarberShopAPI2.Controllers.Request.UsersRequests;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using BarberShopAPI2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI2.Controllers.Endpoints.UserEndPoint;

public static class UsersCreateController
{
    private static IMapper _mapper;
    private static UserManager<User> _userManager;
    private static UserService _userService;

    public static void AddEndPointUserCreate(this WebApplication app)
    {
        var userGroup = app.MapGroup("/user");

        #region Creating an user

        userGroup.MapPost("/create",
            async ([FromServices] Dal<User> dal, [FromBody] UserCreateRequest userCreateRequest) =>
            {
                await _userService.Create(userCreateRequest);
                return Results.Ok("The user has been created successfully!");
            });

        #endregion
        
    }
}