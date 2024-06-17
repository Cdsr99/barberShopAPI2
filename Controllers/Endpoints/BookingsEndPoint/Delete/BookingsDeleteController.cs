using BarberShopAPI2.Controllers.Request.BooksRequest;
using BarberShopAPI2.Data;
using Microsoft.AspNetCore.Mvc;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberShopAPI2.Controllers.Endpoints.Booking;

public static class BookingsDeleteController
{
    public static void AddEndPointBookingDelete(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/booking").WithTags("Booking");

        #region Deleting a book

        booksGroup.MapDelete("/delete/{id}",
            [Authorize]([FromServices] Dal<Models.Booking> booksDal, Dal<Models.Schedule> schedulesDal, int id) =>
            {
                var selectBooking = booksDal.SearchFor(a => a.Id == id);
                if (selectBooking is null) return Results.NotFound("Not Found Booking");

                var selectSchedule = schedulesDal.SearchFor(a => a.Id == selectBooking.SchedulesId);
                if (selectSchedule is null) return Results.NotFound("Not Found Schedule");

                booksDal.Delete(selectBooking);

                selectSchedule.Status = "Available";
                schedulesDal.Update(selectSchedule);

                return Results.NoContent();
            }).WithSwaggerDocumentation("Deleting a book",
            "Deleting a book from the system");

        #endregion
    }
}