using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Features.Items;
using Domain.Models;
using Infrastructure.Extensions;
using LisSkinsClient.Abstractions;
using LisSkinsClient.Model.Requests;
using LisSkinsClient.Model.Responses;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class ApiClient(ILisSkinsClient clientApi, ILogger<ApiClient> logger) : IApiClient
{
    public async Task<List<Item>> FindItemsAsync(FindItemsQuery request, CancellationToken ct)
    {
        var allItems = new List<Item>();
        var req = request.ToApiRequest();

        await FetchAllPagesAsync(req, allItems, ct);

        return allItems;
    }

    private async Task FetchAllPagesAsync(SearchRequest apiReq, List<Item> accumulatedItems, CancellationToken ct)
    {
        var result = await clientApi.SearchAsync(apiReq, ct);

        switch (result)
        {
            case SearchResponse success:
                if (success.Data != null)
                {
                    var items = success.Data.Select(x => x.ToDomain());
                    accumulatedItems.AddRange(items);
                }

                if (!string.IsNullOrEmpty(success.Meta?.NextPage) && apiReq.Names is { Length: > 0 })
                {
                    apiReq.Cursor = success.Meta.NextPage;
                    await FetchAllPagesAsync(apiReq, accumulatedItems, ct);
                }

                break;

            case ErrorResponse error:
                logger.LogWarning("API вернул ошибку при сборке страниц: {Message}", error.Message);
                break;

            default:
                throw new NotSupportedException($"Непредвиденный ответ: {result?.GetType().Name}");
        }
    }
}