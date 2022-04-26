namespace league_of_legends_app.CORE.Models;

public class Champion
{
    public int Id { get; set; } 
    // public int IdSpecie { get; set; }
    public Specie Specie { get; set; }
    public int IdDifficulty { get; set; }
    public int IdRegion { get; set; }
    public string? Name { get; set; }
    public string? Alias { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int PriceBe { get; set; }
    public int PriceRp { get; set; }
    public string? Quote { get; set; }
    public bool IsMelee { get; set; }
}