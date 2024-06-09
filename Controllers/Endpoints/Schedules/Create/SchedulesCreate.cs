using BarberShopAPI2.Controllers.Request;
using BarberShopAPI2.Data;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BarberShopAPI2.Controllers.Endpoints.Schedules.Create;

public static class SchedulesCreate
{
    public static void AddEndPointSchedulesCreate(this WebApplication app)
    {
        var scheduleGroup = app.MapGroup("/schedule/create");
        
        #region Creating schedules for a single time

        scheduleGroup.MapPost("/hour", ([FromServices] Dal<Schedule> dal, [FromBody] ScheduleRequest body) =>
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

        scheduleGroup.MapPost("/day", async ([FromServices] Dal<Schedule> scheduleDal,
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

                await scheduleDal.AddRanger(schedules);

                return Results.Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return Results.BadRequest($"This is the error: {error.Message}");
            }
        }).WithSwaggerDocumentation("Creating schedules for a day", "A new schedule for the day will be created");

        #endregion

        #region Creating schedules for a week

        scheduleGroup.MapPost("/week", async ([FromServices] Dal<Schedule> scheduleDal,
            [FromServices] Dal<Settings> settingsDal,
            [FromBody] ScheduleDayRequest body) =>
        {
            try
            {
                var start = settingsDal.SearchFor(a => a.Parameter == "Start");
                var end = settingsDal.SearchFor(a => a.Parameter == "End");
                var timePerClient = settingsDal.SearchFor(a => a.Parameter == "TimePerClient");
                var daysOff = settingsDal.SearchFor(a => a.Parameter == "DaysOff");

                List<string> daysOfTheWeek = JsonConvert.DeserializeObject<List<string>>(daysOff.Value);
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


                for (int i = 0; i < 7; i++)
                {
                    if (daysOfTheWeek.Contains(day.DayOfWeek.ToString()))
                    {
                        day = day.AddDays(1);
                        i++;

                        if (daysOfTheWeek.Contains(day.DayOfWeek.ToString()))
                        {
                            day = day.AddDays(1);
                            i++;
                        }
                    }

                    while (current <= endConverted)
                    {
                        schedules.Add(new Schedule
                        {
                            Date = day,
                            Hour = current.ToString(@"hh\:mm\:ss")
                        });
                        current = current.Add(timePerClientConverted);
                    }

                    await scheduleDal.AddRanger(schedules);
                    schedules.Clear();
                    day = day.AddDays(1);
                    current = startConverted;
                }

                return Results.Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return Results.BadRequest($"This is the error: {error.Message}");
            }
        }).WithSwaggerDocumentation("Creating schedules for a week", "A new schedule for the week will be created");

        #endregion

        #region Creating schedules for a month

        scheduleGroup.MapPost("/month", async ([FromServices] Dal<Schedule> scheduleDal,
            [FromServices] Dal<Settings> settingsDal,
            [FromBody] ScheduleDayRequest body) =>
        {
            try
            {
                var start = settingsDal.SearchFor(a => a.Parameter == "Start");
                var end = settingsDal.SearchFor(a => a.Parameter == "End");
                var timePerClient = settingsDal.SearchFor(a => a.Parameter == "TimePerClient");
                var daysOff = settingsDal.SearchFor(a => a.Parameter == "DaysOff");

                List<string> daysOfTheWeek = JsonConvert.DeserializeObject<List<string>>(daysOff.Value);
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


                for (int i = 0; i < 30; i++)
                {
                    if (daysOfTheWeek.Contains(day.DayOfWeek.ToString()))
                    {
                        day = day.AddDays(1);
                        i++;

                        if (daysOfTheWeek.Contains(day.DayOfWeek.ToString()))
                        {
                            day = day.AddDays(1);
                            i++;
                        }
                    }

                    while (current <= endConverted)
                    {
                        schedules.Add(new Schedule
                        {
                            Date = day,
                            Hour = current.ToString(@"hh\:mm\:ss")
                        });
                        current = current.Add(timePerClientConverted);
                    }

                    await scheduleDal.AddRanger(schedules);
                    schedules.Clear();
                    day = day.AddDays(1);
                    current = startConverted;
                }

                return Results.Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return Results.BadRequest($"This is the error: {error.Message}");
            }
        }).WithSwaggerDocumentation("Creating schedules for a month", "A new schedule for the month will be created");

        #endregion
    }
}