using BarberShopAPI2.Data;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopAPI2.Controllers.Endpoints.Booking;

public static class BookingsUpdateController
{
    public static void AddEndPointBookingUpdate(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/booking").WithTags("Booking");

        #region Updating a book status

        booksGroup.MapPut("/update/{id}", ([FromServices] Dal<Models.Booking> booksDal, int id) =>
        {
            var selectBooking = booksDal.SearchFor(a => a.Id == id);
            if (selectBooking is null) return Results.NotFound("Not Found Booking");

            selectBooking.Status = "Attended";
            booksDal.Update(selectBooking);

            return Results.NoContent();
        }).WithSwaggerDocumentation("Updating a book status",
            "Updating a book from the system");

        #endregion
    }
}