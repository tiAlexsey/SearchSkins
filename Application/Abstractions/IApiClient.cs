using Application.Features.Items;
using Domain.Models;

namespace Application.Abstractions;

public interface IApiClient
{
    Task<List<Item>> FindItemsAsync(FindItemsQuery request, CancellationToken ct);
}