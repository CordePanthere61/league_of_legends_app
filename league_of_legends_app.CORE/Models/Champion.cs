namespace league_of_legends_app.CORE.Models;

public class Champion : IEquatable<Champion>
{
    public int Id { get; set; } 
    public Specie Specie { get; set; }
    public Difficulty Difficulty { get; set; }
    public Region Region { get; set; }
    public string? Name { get; set; }
    public string? Alias { get; set; }
    public string? ReleaseDate { get; set; }
    public int PriceBe { get; set; }
    public int PriceRp { get; set; }
    public string? Quote { get; set; }
    public bool IsMelee { get; set; }

    public bool Equals(Champion? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Specie.Equals(other.Specie) && Difficulty.Equals(other.Difficulty) && Region.Equals(other.Region) && Name == other.Name && Alias == other.Alias && Nullable.Equals(ReleaseDate, other.ReleaseDate) && PriceBe == other.PriceBe && PriceRp == other.PriceRp && Quote == other.Quote && IsMelee == other.IsMelee;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Champion) obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(Specie);
        hashCode.Add(Difficulty);
        hashCode.Add(Region);
        hashCode.Add(Name);
        hashCode.Add(Alias);
        hashCode.Add(ReleaseDate);
        hashCode.Add(PriceBe);
        hashCode.Add(PriceRp);
        hashCode.Add(Quote);
        hashCode.Add(IsMelee);
        return hashCode.ToHashCode();
    }
}