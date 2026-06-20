namespace Application.Features.Items;

/// /// <summary>
/// Find skin items request.
/// </summary>
/// <param name="Name">Lis-Skins side filter</param>
/// <param name="MinFloat">Lis-Skins side filter</param>
/// <param name="MaxFloat">Lis-Skins side filter</param>
/// <param name="MinPrice">Lis-Skins side filter</param>
/// <param name="MaxPrice">Lis-Skins side filter</param>
/// <param name="OnlyUnlocked">Lis-Skins side filter</param>
/// <param name="MaxLockDays">Lis-Skins side filter</param>
/// <param name="Seeds">Number of seeds</param>
/// <param name="StatTrak">Lis-Skins side filter</param>
/// <param name="Sort"><see cref="Sort"/></param>
/// <param name="Exterior">Lis-Skins side filter</param>
public record FindItemsQuery(
    string? Name,
    double? MinFloat,
    double? MaxFloat,
    decimal? MinPrice,
    decimal? MaxPrice,
    bool? OnlyUnlocked,
    int? MaxLockDays,
    string? Seeds,
    bool? StatTrak,
    Sort Sort = Sort.Price,
    ExteriorType[]? Exterior = null
);

/// <summary>
/// Sort
/// </summary>
public enum Sort
{
    Price = 1,
    Float,
    Seed
}

/// <summary>
/// Exterior
/// </summary>
public enum ExteriorType
{
    Factory_new = 1,
    Minimal_wear,
    Field_tested,
    Well_worn,
    Battle_scarred
}