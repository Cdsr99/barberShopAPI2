using Newtonsoft.Json;
using BarberShopAPI2.Controllers.Request;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
            .WithSwaggerDocumentation("Getting all schedule",
                "A list of all schedules registered in the system will be presented.");

        #endregion

        #region Getting all schedule available

        scheduleGroup.MapGet("/available", ([FromServices] Dal<Schedule> dal) =>
        {
            DateTime today = DateTime.Now;
            var result = dal.SearchForAvailableDays(a => a.Date >= today);
            return Results.Ok(result);
        }).WithSwaggerDocumentation("Getting all schedule available",
            "A list of all available schedules registered in the system will be presented.");

        #endregion

        #region Getting schedule by id

        scheduleGroup.MapGet("/{id}", ([FromServices] Dal<Schedule> dal, int id) =>
        {
            var result = dal.SearchFor(a => a.Id == id);
            return Results.Ok(result);
        }).WithSwaggerDocumentation("Getting schedule by id", "The searched ID will be presented.");

        #endregion

        #region Creating schedules for a single time

        scheduleGroup.MapPost("/create/hour", ([FromServices] Dal<Schedule> dal, [FromBody] ScheduleRequest body) =>
        {
            try
            {
                var scheduleModel = new Schedule();

                scheduleModel.Date = body.date;
                scheduleModel.Hour = body.hour;

                dal.Add(scheduleModel);
                return Results.Ok();
            }
            catch (Exception error)
            {
                return Results.BadRequest(error);
            }
        }).WithSwaggerDocumentation("Creating schedules for a single time", "A new hour schedule will be created");

        #endregion

        #region Creating schedules for a day

        scheduleGroup.MapPost("/create/day", async ([FromServices] Dal<Schedule> scheduleDal,
            [FromServices] Dal<Settings> settingsDal,
            [FromBody] ScheduleDayRequest body) =>
        {
            try
            {
                var start = settingsDal.SearchFor(a => a.Parameter == "Start");
                var end = settingsDal.SearchFor(a => a.Parameter == "End");
                var timePerClient = settingsDal.SearchFor(a => a.Parameter == "TimePerClient");

                if (start == null || end == null || timePerClient == null)
                {
                    return Results.BadRequest("Missing time settings.");
                }

                TimeSpan startConverted = TimeSpan.Zero;
                TimeSpan endConverted = TimeSpan.Zero;
                TimeSpan timePerClientConverted = TimeSpan.Zero;


                if (!TimeSpan.TryParse(start.Value, out startConverted) ||
                    !TimeSpan.TryParse(end.Value, out endConverted) ||
                    !TimeSpan.TryParse(timePerClient.Value, out timePerClientConverted))
                {
                    return Results.BadRequest("Invalid time parameters.");
                }

                DateTime day = body.day;
                List<Schedule> schedules = new List<Schedule>();
                TimeSpan current = startConverted;

                while (current <= endConverted)
                {
                    schedules.Add(new Schedule
                    {
                        Date = day,
                        Hour = current.ToString(@"hh\:mm\:ss")
                    });
                    current = current.Add(timePerClientConverted);
                }
                
                //return Results.Ok(schedules);
                await scheduleDal.AddRanger(schedules);

                /*
                foreach (var schedule in schedules)
                {
                    await dal.AddRanger(schedules);
                    Console.WriteLine("+1");
                }
                */

                return Results.Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return Results.BadRequest($"This is the error: {error.Message}");
            }
        }).WithSwaggerDocumentation("Creating schedules for a day", "A new schedule for the day will be created");

        #endregion
    }
}