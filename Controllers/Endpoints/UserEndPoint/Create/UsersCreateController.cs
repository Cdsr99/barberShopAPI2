using AutoMapper;
using BarberShopAPI2.Controllers.Request.UsersRequests;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using BarberShopAPI2.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BarberShopAPI2.Controllers.Endpoints.UserEndPoint;

public static class UsersCreateController
{
    private static IMapper _mapper;
    private static UserManager<User> _userManager;

    public static void AddEndPointUserCreate(this WebApplication app)
    {
        var userGroup = app.MapGroup("/user");
        var _logger = app.Services.GetRequiredService<ILogger<Program>>();


        #region Creating an user

        userGroup.MapPost("/create",
            async ([FromServices] Dal<User> dal, UserService _userServices,
                [FromBody] UserCreateRequest userCreateRequest) =>
            {
                await _userServices.Create(userCreateRequest);
                return Results.Ok("Usuário cadastrado com sucesso!");
            });

        #endregion
    }
}