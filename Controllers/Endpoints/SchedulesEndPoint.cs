using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI2.Controllers.Endpoints;

public static class SchedulesEndPoint
{
    public static void AddEndPointSchedules(this WebApplication app)
    {
        #region Getting all schedules
        app.MapGet("/schedule/index", ([FromServices] Dal<Schedule> dal) =>
        {
            var schedulesSelected = dal.Show();
            return Results.Ok(schedulesSelected);
        });
        #endregion
        
        #region Getting all available
        app.MapGet("/schedule/available", ([FromServices] Dal<Schedule> dal) =>
        {
            var today = DateTime.Today;
            var result = dal.SearchForAvailableDays(a => a.Date > today);
            return Results.Ok(result);
        });
        #endregion
    }
}