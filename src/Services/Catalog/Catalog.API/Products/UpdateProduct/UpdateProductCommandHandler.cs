﻿
namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile) : ICommand<UpdateProductResult>;

	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}
	public record UpdateProductResult(bool IsSuccess);
	public class UpdateProductCommandHandler (IDocumentSession session)
		: ICommandHandler<UpdateProductCommand, UpdateProductResult>
	{

		public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
			if (product == null)
			{
				throw new ProductNotFoundException(command.Id);
			}
			product.Name = command.Name;
			product.Description = command.Description;
			product.Price = command.Price;
			product.Category = command.Category;
			product.ImageFile = command.ImageFile;
			session.Update(product);
			await session.SaveChangesAsync(cancellationToken);
			return new UpdateProductResult(true);
		}
	}
	
}
