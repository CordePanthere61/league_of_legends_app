namespace league_of_legends_app.CORE.Interfaces;

public interface IWindowAdapter
{
    public Dictionary<string, Action> Commands { get; set; }
}