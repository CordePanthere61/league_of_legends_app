using System.Data;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.DatabaseUtils;

public abstract class Repository<T> where T : class
{
    protected Database _database;
    protected Repository()
    {
        _database = Database.Create();
    }

    public abstract T Handle(DataRow dr);
    
    public abstract Task<T> Find(int Id);

    public abstract Task<List<T>> FindAll();

    public abstract Task<int> Insert(T entity);
    public abstract Task<int> Update(T entity);
}