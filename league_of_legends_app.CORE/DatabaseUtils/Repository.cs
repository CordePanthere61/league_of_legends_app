using System.Data;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.DatabaseUtils;

public abstract class Repository<T> where T : class
{
    protected Database _database;
    
    protected Repository()
    {
        this._database = Database.Create();
    }

    public abstract T Handle(DataRow dr);
    
    public abstract T Find(int Id);

    public abstract Task<List<T>> FindAll();
}