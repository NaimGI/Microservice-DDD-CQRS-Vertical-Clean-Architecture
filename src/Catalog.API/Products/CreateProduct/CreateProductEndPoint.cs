﻿namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFil, decimal Price);

    public record CreateProductResponse(Guid Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProducteCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/product/{response.Id}", response);
            }).WithName("CreateProduct")
              .Produces<CreateProductResponse>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Create Product")
              .WithDescription("Create Product");
        }
    }
}
