namespace league_of_legends_app.CORE.Interfaces;

public interface IWindowAdapter
{
    public Dictionary<string, Action> Commands { get; set; }
    
    public Dictionary<string, Action<int>> CommandsWithId { get; set; }

    public void Error(string message);
    public void Success(string message);
    public void Close();
}