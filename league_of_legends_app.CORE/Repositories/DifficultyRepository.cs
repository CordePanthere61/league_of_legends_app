using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class DifficultyRepository : Repository<Difficulty>
{
    
    public override Difficulty Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Difficulty>> FindAll()
    {
        throw new NotImplementedException();
    }
    
    public override Difficulty Handle(DataRow dr)
    {
        Difficulty difficulty = new Difficulty();
        difficulty.Id = dr.Field<int>("difficulty.Id");
        difficulty.Name = dr.Field<string>("difficulty.Name");
        return difficulty;
    }
}