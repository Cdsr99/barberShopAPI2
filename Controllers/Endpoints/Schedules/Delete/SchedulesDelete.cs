using BarberShopAPI2.Controllers.Request;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI2.Controllers.Endpoints.Schedules.Delete;

public static class SchedulesDelete
{
    public static void AddEndPointSchedulesDelete(this WebApplication app)
    {
        var scheduleGroup = app.MapGroup("/schedule/delete");

        #region Deleting by Id

        scheduleGroup.MapDelete("/{id}", ([FromServices] Dal<Schedule> dal, int id) =>
            {
                var select = dal.SearchFor(a => a.Id == id);
                if (select == null) return Results.NotFound();

                dal.Delete(select);
                return Results.Ok();
            })
            .WithSwaggerDocumentation("Deleting by Id",
                "A schedule will be delete by its id");

        #endregion

        #region Deleting schedules for a day

        scheduleGroup.MapDelete("/day",  ([FromServices] Dal<Schedule> scheduleDal, [FromBody] ScheduleDayRequest body) =>
        {
            try
            {
                DateTime day = body.day;
                var schedulesDay = scheduleDal.SearchFor(a => a.Date == day);
                scheduleDal.Delete(schedulesDay);

                return Results.Ok(schedulesDay);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return Results.BadRequest($"This is the error: {error.Message}");
            }
        }).WithSwaggerDocumentation("Deleting schedules for a day", "A day of schedule will be delete");

        #endregion
    }
}