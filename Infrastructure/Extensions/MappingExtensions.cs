using Application.Features.Items;
using Domain.Models;
using LisSkinsClient.Model.Requests;
using LisSkinsClient.Model.Responses;

namespace Infrastructure.Extensions;

public static class MappingExtensions
{
    public static SearchRequest ToApiRequest(this FindItemsQuery request)
    {
        var names = GetFullName(request.Name, request.Exterior, request.StatTrak);
        var unlockDays = GetDayArray(request.MaxLockDays);

        return new SearchRequest(Names: names,
            FloatFrom: request.MinFloat,
            FloatTo: request.MaxFloat,
            Sort: null,
            MinPrice: request.MinPrice,
            MaxPrice: request.MaxPrice,
            UnlockDays: unlockDays,
            OnlyUnlocked: request.OnlyUnlocked,
            Game: LisSkinsGames.csgo
        );
    }

    public static Item ToDomain(this SkinItem skinItem)
    {
        var floatStr = skinItem.Float.ToString();
        double? floatValue = null;
        if (floatStr is { Length: > 0 })
            floatValue = double.TryParse(floatStr.AsSpan(0, 8), out var result) ? result : null;

        return new Item
        {
            Id = skinItem.Id,
            Name = skinItem.Name,
            Price = skinItem.Price,
            Seed = skinItem.Seed,
            Paint = skinItem.Paint,
            Inspect = skinItem.Inspect,
            Float = floatValue,
            Tag = skinItem.Tag,
            Stickers = skinItem.Stickers?.Select(x => x.ToDomain()).ToList() ?? []
        };
    }

    private static Sticker ToDomain(this StickerItem sticker) => new()
    {
        Name = sticker.Name,
        FullName = sticker.FullName,
        Image = sticker.Image,
        Wear = sticker.Wear,
        Slot = sticker.Slot
    };

    private static int[] GetDayArray(int? day)
    {
        if (day == null)
            return [];

        var result = new int[day.Value + 1];
        for (var i = 0; i < result.Length; i++)
            result[i] = i;
        return result;
    }

    private static string[] GetFullName(string? fullName, ExteriorType[]? types = null, bool? isStatTrack = null)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return [];

        var hasStar = fullName.Contains('★');
        fullName = fullName.Replace("★", "").Trim();

        List<string> baseNames = isStatTrack switch
        {
            true => hasStar
                ? [$"★ StatTrak™ {fullName}"]
                : [$"StatTrak™ {fullName}"],
            false => hasStar
                ? [$"★ {fullName}"]
                : [$"{fullName}"],
            null => hasStar
                ? [$"★ StatTrak™ {fullName}", $"★ {fullName}"]
                : [$"StatTrak™ {fullName}", $"{fullName}"]
        };

        var exteriors = types is { Length: > 0 }
            ? types
            :
            [
                ExteriorType.Factory_new,
                ExteriorType.Minimal_wear,
                ExteriorType.Field_tested,
                ExteriorType.Well_worn,
                ExteriorType.Battle_scarred
            ];

        return baseNames
            .SelectMany(name => exteriors.Select(type => $"{name} {type.GetName()}"))
            .ToArray();
    }
}