using System.Text.Json.Serialization;

namespace LisSkinsClient.Model.Responses;

/// <summary>
/// Результат получения баланса
/// </summary>
public record BalanceResponse(
    [property: JsonPropertyName("data")]
    Balance Data) : BaseResponse;

/// <summary>
/// Баланс
/// </summary>
public record Balance(
    [poperty: JsonPropertyName("balance")]
    decimal Available,
    [property: JsonPropertyName("balance_locked")]
    decimal Locked);