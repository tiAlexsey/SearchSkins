using LisSkinsClient.Model.Requests;
using LisSkinsClient.Model.Responses;

namespace LisSkinsClient.Abstractions;

public interface ILisSkinsClient
{
    Task<BaseResponse> SearchAsync(SearchRequest request, CancellationToken ct = default);
    Task<BaseResponse> GetBalanceAsync(CancellationToken ct = default);
    Task<BaseResponse> PurchaseAsync(PurchaseRequest request, CancellationToken ct = default);
}