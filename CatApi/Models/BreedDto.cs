namespace CatApi.Models;

public class BreedDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Temperament { get; set; }
}
