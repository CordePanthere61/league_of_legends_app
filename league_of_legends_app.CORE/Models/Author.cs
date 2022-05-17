namespace league_of_legends_app.CORE.Models;

public class Author : IEquatable<Author>
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public bool Equals(Author? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Author) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}