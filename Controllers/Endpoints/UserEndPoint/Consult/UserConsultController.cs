using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI2.Controllers.Endpoints.UserEndPoint.Consult;

public static class UserConsultController
{
    public static void AddEndPointUserConsult(this WebApplication app)
    {
        var userGroup = app.MapGroup("/user").WithTags("User");

        #region Getting users

        userGroup.MapGet("/index",
            [Authorize] ([FromServices] Dal<User> dal) =>
            {
                var select = dal.Show();
                return select;
            });

        #endregion
    }
}