namespace Domain.Models;

public class Item
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Inspect { get; set; }
    public double? Float { get; set; }
    public int? Seed { get; set; }
    public int? Paint { get; set; }
    public string? Tag { get; set; }
    public List<Sticker>? Stickers { get; set; }
    public string Screenshot => $"https://app.lis-skins.com/ru/market/screenshot/request/{Id}/";
}