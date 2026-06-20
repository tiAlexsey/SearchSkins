namespace LisSkinsClient.Model.Requests;

/// <summary>
/// Запрос на покупку предметов
/// </summary>
/// <param name="Ids">Список Id предметов</param>
/// <param name="SteamLink">Ссылка на обмен в steam</param>
public record PurchaseRequest(
    List<int> Ids,
    string SteamLink
);