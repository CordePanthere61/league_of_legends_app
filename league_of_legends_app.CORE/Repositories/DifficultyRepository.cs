using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class DifficultyRepository : Repository<Difficulty>
{
    private const string BaseSelectAll = "select d.id \"difficulty.Id\", d.name \"difficulty.Name\" from difficulty d";
    
    public override Difficulty Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Difficulty>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }
    
    public override Difficulty Handle(DataRow dr)
    {
        Difficulty difficulty = new Difficulty();
        difficulty.Id = dr.Field<int>("difficulty.Id");
        difficulty.Name = dr.Field<string>("difficulty.Name");
        return difficulty;
    }
}