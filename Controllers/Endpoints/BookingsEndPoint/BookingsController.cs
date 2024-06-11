using BarberShopAPI2.Controllers.Request.BooksRequest;
using BarberShopAPI2.Data;
using Microsoft.AspNetCore.Mvc;
using BarberShopAPI2.Models;

namespace BarberShopAPI2.Controllers.Endpoints.Booking;

public static class BookingsController
{
    public static void AddEndPointBooking(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/booking");

        #region Getting all books

        booksGroup.MapGet("/index", ([FromServices] Dal<Models.Booking> dal) =>
        {
            var select = dal.Show();
            return Results.Ok(select);
        }).WithSwaggerDocumentation("Getting all books",
            "A list of all books registered in the system will be presented.");

        #endregion
        
        #region Creating a book

        booksGroup.MapPost("/create", ([FromServices] Dal<Models.Booking> booksDal, Dal<Models.Schedule> schedulesDal, [FromBody] BooksCreateRequest booksCreateRequest ) =>
        {
            var schedulesId = schedulesDal.SearchFor(a => a.Id == booksCreateRequest.schedulesId);
            if (schedulesId is null ) return Results.NotFound();

            Models.Booking book = new Models.Booking(booksCreateRequest.clientName, booksCreateRequest.clientsPhoneNumber, booksCreateRequest.schedulesId);
            
            booksDal.Add(book);
            schedulesId.Status = "Booked";
            schedulesDal.Update(schedulesId);
            
            
            return Results.Ok();
            
        }).WithSwaggerDocumentation("Creating a book",
            "Create a book in the system");

        #endregion
        
        #region Deleting a book

        booksGroup.MapDelete("/delete/{id}", ([FromServices] Dal<Models.Booking> booksDal, Dal<Models.Schedule> schedulesDal, int id ) =>
        {
            Console.WriteLine($"This is the id: {id} ***************************************************");
            var selectBooking = booksDal.SearchFor(a => a.Id == id);
            if (selectBooking is null ) return Results.NotFound("Not Found Booking");

            Console.WriteLine(selectBooking.SchedulesId);
            
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