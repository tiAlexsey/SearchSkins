using Application.Abstractions;
using Domain;
using Domain.Models;

namespace Application.Features.Items;

public class FindItemsHandler(IApiClient apiClient) : IHandler<FindItemsQuery, PagedResult<Item>>
{
    public async Task<PagedResult<Item>> HandleAsync(FindItemsQuery request, CancellationToken ct = default)
    {
        var items = await apiClient.FindItemsAsync(request, ct);

        IEnumerable<Item> query = items;

        if (!string.IsNullOrWhiteSpace(request.Seeds))
        {
            var filterSeeds = request.Seeds.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.TryParse(x, out var val) ? val : (int?)null)
                .OfType<int>()
                .ToHashSet();

            if (filterSeeds.Count > 0)
            {
                query = query.Where(x => x.Seed != null && filterSeeds.Contains(x.Seed.Value));
            }
        }

        query = request.Sort switch
        {
            Sort.Float => query.OrderBy(x => x.Float),
            Sort.Price => query.OrderBy(x => x.Price),
            Sort.Seed => query.OrderBy(x => x.Seed.HasValue).ThenBy(x => x.Seed)
        };

        var result = query.ToList();

        return new PagedResult<Item>(result, result.Count);
    }
}