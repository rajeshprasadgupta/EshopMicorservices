﻿using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomer : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
		{
			var query = new GetOrdersByCustomerQuery(customerId);
			var result = await sender.Send(query);
			var response = result.Adapt<GetOrdersByCustomerResponse>();
			return Results.Ok<GetOrdersByCustomerResponse>(response);
		})
		.WithName("GetOrdersByCustomer")
		.Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.ProducesProblem(StatusCodes.Status404NotFound)
		.WithSummary("Get Orders By Customer")
		.WithDescription("Get Orders By Customer");
	}
}