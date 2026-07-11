using System.Text.Json.Serialization;

namespace LisSkinsClient.Model.Responses;

/// <summary>
/// Result of search request
/// </summary>
/// <param name="Data"><see cref="SkinItem">Items</see></param>
/// <param name="Meta"><see cref="MetaData">Pagination</see></param>
public record SearchResponse(
    [property: JsonPropertyName("data")]
    List<SkinItem>? Data,
    [property: JsonPropertyName("meta")]
    MetaData? Meta) : BaseResponse;

/// <summary>
/// Pagination
/// </summary>
/// <param name="Count">How many items were found</param>
/// <param name="NextPage">Next page</param>
public record MetaData(
    [property: JsonPropertyName("per_page")]
    int Count,
    [property: JsonPropertyName("next_cursor")]
    string? NextPage);

/// <summary>
/// Skin Item
/// </summary>
/// <param name="Id">Skin ID (used for purchase)</param>
/// <param name="Name">Skin Name</param>
/// <param name="Price">Skin Price</param>
/// <param name="UnlockAt">Skin unlock date</param>
/// <param name="ClassId">Class ID from Steam Inventory. Can be used to get a skin image</param>
/// <param name="CreatedAt">Date the skin was added to our database</param>
/// <param name="Float">Skin Float value (only CS2)</param>
/// <param name="Tag">Skin Name Tag value (only CS2)</param>
/// <param name="Paint">Paint Index (only CS2)</param>
/// <param name="Seed">Paint Seed (only CS2)</param>
/// <param name="Stickers"><see cref="StickerItem">Stickers</see> (only CS2)</param>
public record SkinItem(
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("price")]
    decimal Price,
    [property: JsonPropertyName("unlock_at")]
    string? UnlockAt,
    [property: JsonPropertyName("item_class_id")]
    string? ClassId,
    [property: JsonPropertyName("created_at")]
    string? CreatedAt,
    [property: JsonPropertyName("item_float")]
    double? Float,
    [property: JsonPropertyName("name_tag")]
    string? Tag,
    [property: JsonPropertyName("item_link")]
    string? Inspect,
    [property: JsonPropertyName("item_paint_index")]
    int? Paint,
    [property: JsonPropertyName("item_paint_seed")]
    int? Seed,
    [property: JsonPropertyName("stickers")]
    List<StickerItem>? Stickers);

/// <summary>
/// Stickers Item
/// </summary>
/// <param name="Name">Sticker Name</param>
/// <param name="Image">Sticker Image</param>
public record StickerItem(
    [property: JsonPropertyName("name")]
    string? Name,
    [property: JsonPropertyName("name_full")]
    string? FullName,
    [property: JsonPropertyName("image")]
    string? Image,
    [property: JsonPropertyName("wear")]
    double? Wear,
    [property: JsonPropertyName("slot")]
    int? Slot);