using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;
using MediatR;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductsRequest(int? PageNumber , int? PageSize);
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductQuery>();
                var result = await sender.Send(query);

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);

            }).WithName("GetProducts")
              .Produces<CreateProductResponse>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get Product")
              .WithDescription("Get Product");

        }
    }
}
