using Catalog.API.Models;
using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProducteCommand(string Name,List<string> Category,string Description, string ImageFil, decimal Price)
          : ICommand<CreateProducteResult>;
    public record CreateProducteResult(Guid Id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProducteCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFil).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProducteCommand, CreateProducteResult>
    {
        public async Task<CreateProducteResult> Handle(CreateProducteCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,  
                ImageFile = request.ImageFil,
                Price = request.Price,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProducteResult(product.Id);
        }
    }
}
