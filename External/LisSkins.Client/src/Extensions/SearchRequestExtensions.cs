using System.Globalization;
using System.Web;
using LisSkinsClient.Model.Requests;

namespace LisSkinsClient.Extensions;

internal static class SearchRequestExtensions
{
    public static string? ToParams(this SearchRequest request)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (request.Sort != null)
            query["sort"] = request.Sort.ToString();
        if (request.Cursor != null)
            query["cursor"] = request.Cursor;
        if (request.FloatFrom != null)
            query["float_from"] = request.FloatFrom.Value.ToString(CultureInfo.InvariantCulture);
        if (request.FloatTo != null)
            query["float_to"] = request.FloatTo.Value.ToString(CultureInfo.InvariantCulture);
        if (request.MinPrice != null)
            query["price_from"] = request.MinPrice.Value.ToString(CultureInfo.InvariantCulture);
        if (request.MaxPrice != null)
            query["price_to"] = request.MaxPrice.Value.ToString(CultureInfo.InvariantCulture);

        if (request.Names != null)
            foreach (var name in request.Names)
                query.Add("names[]", name);

        if (request.UnlockDays != null)
            foreach (var day in request.UnlockDays)
                query.Add("unlock_days[]", day.ToString());

        query["game"] = request.Game.ToString();
        return query.ToString();
    }
}