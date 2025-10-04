namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);
    

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await mediator.Send(command, cancellationToken);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{result.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Creates a new product.")
        .WithSummary("Creates a new product.");
        
    }
}