using BarberShopAPI2.Controllers.Request.BooksRequest;
using BarberShopAPI2.Data;
using Microsoft.AspNetCore.Mvc;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberShopAPI2.Controllers.Endpoints.Booking;


public static class BookingsConsultController
{

    public static void AddEndPointBookingConsult(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/booking").WithTags("Booking");

        #region Getting all books

        booksGroup.MapGet("/index", ([FromServices] Dal<Models.Booking> dal) =>
        {
            var select = dal.Show();
            return Results.Ok(select);
        }).WithSwaggerDocumentation("Getting all books",
            "A list of all books registered in the system will be presented.");

        #endregion
        
        #region Getting books by id

        booksGroup.MapGet("/index/{id}", ([FromServices] Dal<Models.Booking> dal, int id) =>
        {
            var select = dal.SearchFor(a => a.Id == id);
            return Results.Ok(select);
        }).WithSwaggerDocumentation("Getting all books by id", "A list of all books registered in the system will be presented.");

        #endregion

    }
}