using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class SpecieRepository : Repository<Specie>
{

    public override Specie Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Specie>> FindAll()
    {
        throw new NotImplementedException();
    }
    
    public override Specie Handle(DataRow dr)
    {
        Specie specie = new Specie();
        specie.Id = dr.Field<int>("specie.Id");
        specie.Name = dr.Field<string>("specie.Name");
        return specie;
    }
}