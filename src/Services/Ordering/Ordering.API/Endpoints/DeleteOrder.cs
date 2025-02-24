﻿
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
		{
			var command = new DeleteOrderCommand(id);
			var result = await sender.Send(command);
			var response = result.Adapt<DeleteOrderResponse>();
			return Results.Ok<DeleteOrderResponse>(response);
		}).WithName("DeleteOrder")
		.Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.ProducesProblem(StatusCodes.Status404NotFound)
		.WithSummary("DeleteOrder")
		.WithDescription("DeleteOrder");
	}

}