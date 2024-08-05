using Swashbuckle.AspNetCore.Annotations;

namespace BarberShopAPI2.Controllers.Endpoints;

public static class SwaggerExtensions
{
    public static RouteHandlerBuilder WithSwaggerDocumentation(this RouteHandlerBuilder builder, string summary,
        string description)
    {
        return builder
            .WithMetadata(new SwaggerOperationAttribute(summary, description))
            .Produces(200)
            .Produces(400)
            .Produces(404);
    }
}