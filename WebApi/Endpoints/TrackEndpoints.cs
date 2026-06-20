using Application.Abstractions;
using Application.Features.Items;
using Domain;
using Domain.Models;
using WebApi.Absractions;

namespace WebApi.Endpoints;

public class TrackEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("track").WithTags("track").RequireAuthorization();

        group.MapGet("/clean-vice",
            async (IHandler<FindItemsQuery, PagedResult<Item>> handler, CancellationToken ct) =>
            {
                var query = new FindItemsQuery(
                    Name: "★ Sport Gloves | Vice",
                    MinFloat: null,
                    MaxFloat: null,
                    MinPrice: null,
                    MaxPrice: 2000,
                    OnlyUnlocked: null,
                    MaxLockDays: null,
                    Seeds:
                    "0 1 22 30 34 35 45 49 73 81 123 129 134 150 180 204 230 231 240 269 281 288 327 340 348 372 405 406 407 418 432 470 479 495 496 515 527 550 563 566 583 624 647 664 694 706 722 737 738 743 744 746 753 773 802 816 872 886 896 919 920 942 943 956 979 983 984 996",
                    StatTrak: null,
                    Sort: Sort.Price,
                    Exterior: [ExteriorType.Minimal_wear]
                );

                return Results.Ok(await handler.HandleAsync(query, ct));
            });

        group.MapGet("/just-vice",
            async (IHandler<FindItemsQuery, PagedResult<Item>> handler, CancellationToken ct) =>
            {
                var query = new FindItemsQuery(
                    Name: "★ Sport Gloves | Vice",
                    MinFloat: null,
                    MaxFloat: null,
                    MinPrice: null,
                    MaxPrice: null,
                    OnlyUnlocked: null,
                    MaxLockDays: null,
                    Seeds: null,
                    StatTrak: null,
                    Sort: Sort.Price,
                    Exterior: [ExteriorType.Minimal_wear]
                );

                return Results.Ok(await handler.HandleAsync(query, ct));
            });

        group.MapGet("/skeleton-sapphire",
            async (IHandler<FindItemsQuery, PagedResult<Item>> handler, CancellationToken ct) =>
            {
                var query = new FindItemsQuery(
                    Name: "★ Skeleton Knife | Doppler Sapphire",
                    MinFloat: null,
                    MaxFloat: null,
                    MinPrice: null,
                    MaxPrice: null,
                    OnlyUnlocked: null,
                    MaxLockDays: null,
                    Seeds:
                    "29 87 101 152 159 171 208 244 330 375 398 401 403 456 577 578 607 648 660 683 936 940 971 985",
                    StatTrak: null
                );

                return Results.Ok(await handler.HandleAsync(query, ct));
            });

        group.MapGet("/blue-deagle",
            async (IHandler<FindItemsQuery, PagedResult<Item>> handler, CancellationToken ct) =>
            {
                var query = new FindItemsQuery(
                    Name: "Desert Eagle | Heat Treated",
                    MinFloat: null,
                    MaxFloat: null,
                    MinPrice: null,
                    MaxPrice: null,
                    OnlyUnlocked: null,
                    MaxLockDays: null,
                    Seeds: "490 148 69 704 567 308",
                    StatTrak: null,
                    Sort: Sort.Price,
                    Exterior: [ExteriorType.Factory_new, ExteriorType.Minimal_wear, ExteriorType.Field_tested]
                );

                return Results.Ok(await handler.HandleAsync(query, ct));
            });
    }
}