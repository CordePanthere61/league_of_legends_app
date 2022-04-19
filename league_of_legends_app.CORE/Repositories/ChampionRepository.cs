using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class ChampionRepository : Repository<Champion>
{

    public override Champion Find(int id)
    {
        Champion champion = _database.SelectSingle("select id \"champion.id\", name \"champion.name\" from champion where id = @id", this, new Parameter("id", id));
        return champion;
    }

    public override List<Champion> FindAll()
    {
        throw new NotImplementedException();
    }
    
    public override Champion Handle(DataRow dr)
    {
        Champion champion = new Champion();
        champion.Id = dr.Field<int>("champion.id");
        champion.Name = dr.Field<string>("champion.name");
        return champion;
    }
}