using System.Net.Http.Json;
using LisSkinsClient.Abstractions;
using LisSkinsClient.Extensions;
using LisSkinsClient.Model.Requests;
using LisSkinsClient.Model.Responses;
using Microsoft.Extensions.Logging;

namespace LisSkinsClient.Services;

internal class ApiClient(ILogger<ApiClient> logger, HttpClient client) : ILisSkinsClient
{
    private const string SearchEndpoint = "market/search";
    private const string BuyEndpoint = "market/buy";
    private const string BalanceEndpoint = "user/balance";

    public Task<BaseResponse> GetBalanceAsync(CancellationToken ct = default) =>
        SendRequestAsync<BalanceResponse>(() =>
            client.GetAsync(BalanceEndpoint, ct), ct);

    public Task<BaseResponse> PurchaseAsync(PurchaseRequest request, CancellationToken ct = default) =>
        SendRequestAsync<PurchaseResponse>(() =>
            client.PostAsJsonAsync(BuyEndpoint, request, cancellationToken: ct), ct);

    public Task<BaseResponse> SearchAsync(SearchRequest request, CancellationToken ct = default)
    {
        var paramsString = request.ToParams();
        var url = string.IsNullOrEmpty(paramsString)
            ? SearchEndpoint
            : $"{SearchEndpoint}?{paramsString}";

        return SendRequestAsync<SearchResponse>(() => client.GetAsync(url, ct), ct);
    }

    private async Task<BaseResponse> SendRequestAsync<TResponse>(
        Func<Task<HttpResponseMessage>> httpRequestFunc, CancellationToken ct = default) where TResponse : BaseResponse
    {
        try
        {
            var response = await httpRequestFunc();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: ct);
                return result ?? new BaseResponse();
            }

            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
                return errorResult ?? new ErrorResponse(Message: "Пустой ответ ошибки от сервера");
            }

            var rawErrorText = await response.Content.ReadAsStringAsync(ct);
            logger.LogWarning("API вернул ошибку без JSON (Статус: {StatusCode}). Текст: {Text}",
                response.StatusCode, rawErrorText);

            return new ErrorResponse(Message: $"Ошибка сервера: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Критическая ошибка при выполнении запроса: {Message}", ex.Message);
            throw;
        }
    }
}