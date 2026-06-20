namespace LisSkinsClient.Model.Requests;

/// <summary>
/// This method allows you to search for skins available for purchase.
/// </summary>
public record SearchRequest(
    string[]? Names,
    double? FloatFrom,
    double? FloatTo,
    LisSkinsSort? Sort,
    decimal? MinPrice,
    decimal? MaxPrice,
    int[]? UnlockDays,
    bool? OnlyUnlocked,
    LisSkinsGames Game = LisSkinsGames.csgo)
{
    public string? Cursor { get; set; }
}
/// <summary>
/// Игра
/// </summary>
public enum LisSkinsGames
{
    csgo,
    dota2,
    rust
}

/// <summary>
/// Сортировка поиска
/// </summary>
public enum LisSkinsSort
{
    oldest,
    newest,
    lowest_price,
    highest_price
}