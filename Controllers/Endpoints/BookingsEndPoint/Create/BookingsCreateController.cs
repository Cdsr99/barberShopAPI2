using BarberShopAPI2.Controllers.Request.BooksRequest;
using BarberShopAPI2.Data;
using Microsoft.AspNetCore.Mvc;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberShopAPI2.Controllers.Endpoints.Booking;

public static class BookingsCreateController
{
    public static void AddEndPointBookingCreate(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/booking").WithTags("Booking");
        
        #region Creating a book

        booksGroup.MapPost("/create", ([FromServices] Dal<Models.Booking> booksDal, Dal<Models.Schedule> schedulesDal, [FromBody] BooksCreateRequest booksCreateRequest ) =>
        {
            try
            {
                var schedulesId = schedulesDal.SearchFor(a => a.Id == booksCreateRequest.schedulesId);
                if (schedulesId is null) return Results.NotFound();

                Models.Booking book = new Models.Booking(booksCreateRequest.clientName, booksCreateRequest.clientsPhoneNumber, booksCreateRequest.schedulesId);

                booksDal.Add(book);
                schedulesId.Status = "Booked";
                schedulesDal.Update(schedulesId);


                return Results.Ok();
            }
            catch (Exception error)
            {
                return Results.BadRequest(error);
            }

            
        }).WithSwaggerDocumentation("Creating a book",
            "Create a book in the system");

        #endregion
        
    }
}