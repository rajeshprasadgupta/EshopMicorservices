﻿namespace Catalog.API.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
		}
	}
	public record DeleteProductResult(bool IsSuccess);
	public class DeleteProductCommandHandler(IDocumentSession session)
		: ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
		{ 
			var product = await session.LoadAsync<Product>(command.Id);
			if (product == null)
			{
				throw new ProductNotFoundException(command.Id);
			}
			session.Delete<Product>(product.Id);
			await session.SaveChangesAsync(cancellationToken);
			return new DeleteProductResult(true);
		}
	}
}
