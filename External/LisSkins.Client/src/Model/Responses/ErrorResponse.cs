using System.Text.Json.Serialization;

namespace LisSkinsClient.Model.Responses;

/// <summary>
/// Ответ от сервера с ошибкой
/// </summary>
public record ErrorResponse(
    [property: JsonPropertyName("error")]
    string Message) : BaseResponse;