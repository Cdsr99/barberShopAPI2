using BarberShopAPI2.Controllers.Request;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Mvc;


namespace BarberShopAPI2.Controllers.Endpoints;

public static class SchedulesEndPoint
{
    public static void AddEndPointSchedules(this WebApplication app)
    {
        var scheduleGroup = app.MapGroup("/schedule");
        
            #region Getting all schedule
            scheduleGroup.MapGet("/index", ([FromServices] Dal<Schedule> dal) =>
            {
              var result = dal.Show();
              return Results.Ok(result);
            })
            .WithSwaggerDocumentation("Getting all schedule","A list of all schedules registered in the system will be presented.");
            #endregion
    
            #region Getting all schedule available
            scheduleGroup.MapGet("/available", ([FromServices] Dal<Schedule> dal) =>
            {
                var today = DateTime.Today;
                var result = dal.SearchForAvailableDays(a => a.Date > today);
                return Results.Ok(result);
            }).WithSwaggerDocumentation("Getting all schedule available","A list of all available schedules registered in the system will be presented.");
            #endregion
            
            #region Getting schedule by id
            scheduleGroup.MapGet("/{id}", ([FromServices] Dal<Schedule> dal, int id) =>
            {
                var result = dal.SearchFor(a => a.Id == id);
                return Results.Ok(result);
            }).WithSwaggerDocumentation("Getting schedule by id","The searched ID will be presented.");
            #endregion
            
            #region Creating an available schedule
            scheduleGroup.MapPost("/index", ([FromServices] Dal<Schedule> dal, [FromBody] ScheduleRequest body) =>
            {
                var scheduleModel = new Schedule(body.date, body.hour);
                dal.Add(scheduleModel);
                return Results.Ok();
            }).WithSwaggerDocumentation("Creating an available schedule","A new schedule will be created");
            #endregion
            
    }
}