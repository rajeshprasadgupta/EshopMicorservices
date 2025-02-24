﻿using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;
namespace Ordering.API.Endpoints;

public record GetOrdersRequest(PaginationRequest PaginationRequest);

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public class GetOrders : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
		{
			var query = new GetOrdersQuery(request);
			var result = await sender.Send(query);
			var response = result.Adapt<GetOrdersResponse>();
			return Results.Ok<GetOrdersResponse>(response);
		})
		.WithName("GetOrders")
		.WithSummary("Get Orders")
		.WithDescription("Get Orders")
		.Produces<GetOrdersResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.ProducesProblem(StatusCodes.Status404NotFound);
	}
}
