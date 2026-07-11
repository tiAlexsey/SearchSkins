using Application.Abstractions;
using Application.Features.Items.FindItems;
using Domain;
using Domain.Models;
using WebApi.Absractions;

namespace WebApi.Endpoints;

public class ItemEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("item").WithTags("item").RequireAuthorization();

        group.MapGet("/find", async ([AsParameters] FindItemsQuery query, IHandler<FindItemsQuery,
                PagedResult<Item>> handler, CancellationToken ct) =>
            Results.Ok(await handler.HandleAsync(query, ct))
        );
    }
}