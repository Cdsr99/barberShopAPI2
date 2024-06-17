using AutoMapper;
using BarberShopAPI2.Controllers.Request.UsersRequests;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BarberShopAPI2.Controllers.Endpoints;

public static class UsersUpdateController
{
    private static IMapper _mapper;
    private static UserManager<User> _userManager;

    public static void AddEnPointUserUpdate(this WebApplication app)
    {
        var userGroup = app.MapGroup("/user").WithTags("User");

        #region Updating User

        userGroup.MapPut("/update",
            async ([FromServices] Dal<User> dal, [FromBody] UserUpdateRequest userUpdateRequest) =>
            {
                var user = dal.SearchFor(a => a.UserName == userUpdateRequest.UserName);
                if (user == null) return Results.NotFound();


                if (userUpdateRequest.Email != null)
                {
                    user.Email = userUpdateRequest.Email;
                    user.NormalizedUserName = userUpdateRequest.Email.ToUpper();
                    user.EmailConfirmed = false;
                }

                if (userUpdateRequest.Password != null)
                {
                    user.PasswordHash = userUpdateRequest.Password;
                }

                if (userUpdateRequest.PhoneNumber != null)
                {
                    user.PhoneNumber = userUpdateRequest.PhoneNumber;
                }

                if (userUpdateRequest.Name != null)
                {
                    user.Name = userUpdateRequest.Name;
                }

                if (userUpdateRequest.Profile != null)
                {
                    user.Profile = userUpdateRequest.Profile;
                }

                dal.Update(user);

                return Results.Ok("User updated with success");
            });

        #endregion
    }
}