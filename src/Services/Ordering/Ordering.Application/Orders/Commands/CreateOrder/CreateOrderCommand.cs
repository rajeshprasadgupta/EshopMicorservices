﻿
namespace Ordering.Application.Orders.Commands.CreateOrder;
public record CreateOrderResult(Guid Id);
public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
	public CreateOrderCommandValidator()
	{
		RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
		RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
		RuleFor(x => x.Order.OrderItems).Must(x => x.Count > 0).WithMessage("At least one item is required");
	}
}