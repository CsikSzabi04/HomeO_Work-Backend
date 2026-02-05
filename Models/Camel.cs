namespace CamelApp.API.Models;

public class Camel{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public int HumpCount { get; set; }
    public DateTime? LastFed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}