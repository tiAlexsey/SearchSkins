using Application.Features.Items;

namespace Infrastructure.Extensions;

public static class ExteriorTypeExtensions
{
    public static string GetName(this ExteriorType type) =>
        type switch
        {
            ExteriorType.Factory_new => "(Factory New)",
            ExteriorType.Minimal_wear => "(Minimal Wear)",
            ExteriorType.Field_tested => "(Field-Tested)",
            ExteriorType.Well_worn => "(Well-Worn)",
            ExteriorType.Battle_scarred => "(Battle-Scarred)",
            _ => string.Empty
        };
}