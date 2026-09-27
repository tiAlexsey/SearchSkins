using Application.Abstractions;
using Application.Features.Items.FindItems;
using Domain;
using Domain.Models;
using WebApi.Abstractions;

namespace WebApi.Endpoints;

public class ItemEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("item").WithTags("item").RequireAuthorization();

        group.MapGet("/find", async ([AsParameters] FindItemsQuery query, IHandler<FindItemsQuery,
                    PagedResult<Item>> handler, CancellationToken ct) =>
                Results.Ok(await handler.HandleAsync(query, ct))
            )
            .WithName("find-items")
            .WithSummary("Поиск предметов")
            .WithDescription(
                "Ищет предметы по фильтрам и возвращает результаты с общим количеством найденных предметов." +
                "Требуется авторизация.")
            .Produces<PagedResult<Item>>()
            .Produces(StatusCodes.Status401Unauthorized);
    }
}