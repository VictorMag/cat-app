namespace CatApi.Models;

public class CatImageDto
{
    public string Id { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
}
