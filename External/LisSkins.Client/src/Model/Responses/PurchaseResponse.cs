using System.Text.Json.Serialization;

namespace LisSkinsClient.Model.Responses;

/// <summary>
/// </summary>
public record PurchaseResponse(
    [property: JsonPropertyName("data")]
    PurchaseData Data) : BaseResponse;

/// <summary>
/// Данные о покупке
/// </summary>
public record PurchaseData(
    [property: JsonPropertyName("purchase_id")]
    int PurchaseId,
    [property: JsonPropertyName("steam_id")]
    string SteamId,
    [property: JsonPropertyName("created_at")]
    DateTime CreatedAt,
    [property: JsonPropertyName("custom_id")]
    string? CustomId,
    [property: JsonPropertyName("skins")]
    List<SkinData> Skins);

/// <summary>
/// Информация о купленном скине
/// </summary>
public record SkinData(
    [property: JsonPropertyName("id")]
    int Id,
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("item_float")]
    double? ItemFloat,
    [property: JsonPropertyName("unlock_at")]
    DateTime? UnlockAt,
    [property: JsonPropertyName("item_asset_id")]
    string? ItemAssetId,
    [property: JsonPropertyName("price")]
    decimal Price,
    [property: JsonPropertyName("return_charged_commission")]
    decimal? ReturnChargedCommission,
    [property: JsonPropertyName("status")]
    StatusType Status,
    [property: JsonPropertyName("return_reason")]
    ReturnReason? ReturnReason,
    [property: JsonPropertyName("error")]
    ErrorType? Error,
    [property: JsonPropertyName("steam_trade_offer_id")]
    string? SteamTradeOfferId,
    [property: JsonPropertyName("steam_trade_offer_created_at")]
    DateTime? SteamTradeOfferCreatedAt,
    [property: JsonPropertyName("steam_trade_offer_expiry_at")]
    DateTime? SteamTradeOfferExpiryAt,
    [property: JsonPropertyName("steam_trade_offer_finished_at")]
    DateTime? SteamTradeOfferFinishedAt);

/// <summary>
/// Статусы
/// </summary>
public enum StatusType
{
    wait_unlock,
    wait_accept,
    accepted,
    @return,
    processing,
    wait_withdraw
}

/// <summary>
/// Причина возврата
/// </summary>
public enum ReturnReason
{
    @null,
    wait_withdraw_timeout,
    manual_cancel,
    trade_timeout,
    trade_create_error,
    trade_canceled,
    rollback_user,
    rollback_supplier,
}

/// <summary>
/// Ошибка
/// </summary>
public enum ErrorType
{
    @null,
    unknown_error,
    invalid_trade_url,
    user_cant_trade,
    private_inventory,
    user_trade_ban,
    user_inventory_full
}