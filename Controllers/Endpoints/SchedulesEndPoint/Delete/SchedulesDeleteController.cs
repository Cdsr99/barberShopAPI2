using System.ComponentModel.DataAnnotations;
using BarberShopAPI2.Controllers.Request;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI2.Controllers.Endpoints.Schedules.Delete;

public static class SchedulesDeleteController
{
    public static void AddEndPointSchedulesDelete(this WebApplication app)
    {
        var scheduleGroup = app.MapGroup("/schedule/delete");

        #region Deleting by Id

        scheduleGroup.MapDelete("/{id}", [Authorize] ([FromServices] Dal<Schedule> dal, int id) =>
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

        scheduleGroup.MapDelete("/day",
            [Authorize] ([FromServices] Dal<Schedule> scheduleDal, [FromBody] ScheduleDayRequest body) =>
            {
                try
                {
                    DateTime day = body.day;
                    var schedulesDay = scheduleDal.SearchForDay(a => a.Date == day);
        
                    foreach (var schedule in schedulesDay) scheduleDal.Delete(schedule);
                    
                    return Results.Ok();
                }
                catch (Exception error)
                {
                    //Console.WriteLine(error);
                    Console.WriteLine("Inner Exception: " + error.InnerException?.Message);
                    return Results.BadRequest($"An error occurred: {error.Message}");
                }
            }).WithSwaggerDocumentation("Deleting schedules for a day", "A day of schedule will be delete");

        #endregion
        
        #region Deleting schedules between days

        scheduleGroup.MapDelete("/day/between",
            [Authorize] ([FromServices] Dal<Schedule> scheduleDal, [FromBody] ScheduleDayBetweenRequest scheduleRequest) =>
            {
                try
                {
                    var schedulesDay = scheduleDal.SearchForDay(a => a.Date >= scheduleRequest.startDay && a.Date <= scheduleRequest.endDay);

                    foreach (var schedule in schedulesDay) scheduleDal.Delete(schedule);
                    
                    return Results.Ok();
                }
                catch (Exception error)
                {
                    //Console.WriteLine(error);
                    Console.WriteLine("Inner Exception: " + error.InnerException?.Message);
                    return Results.BadRequest($"An error occurred: {error.Message}");
                }
            }).WithSwaggerDocumentation("Deleting schedules between days", "The schedules between one day to another will be deleted");

        #endregion
        
        
    }
}