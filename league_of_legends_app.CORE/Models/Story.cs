namespace league_of_legends_app.CORE.Models;

public class Story
{
    public int Id { get; set; }
    public Region Region { get; set; }
    public Author Author { get; set; }
    public string? Name { get; set; }
    public string? Text { get; set; }
}