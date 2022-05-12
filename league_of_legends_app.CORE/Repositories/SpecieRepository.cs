using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class SpecieRepository : Repository<Specie>
{
    private const string BaseSelectAll = "select s.id \"specie.Id\", s.name \"specie.Name\" from specie s";

    public  override Task<List<Specie>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }

    public override Specie Handle(DataRow dr)
    {
        Specie specie = new Specie();
        specie.Id = dr.Field<int>("specie.Id");
        specie.Name = dr.Field<string>("specie.Name");
        return specie;
    }
    
    #region NotImplemented
    
    public override Task<int> Insert(Specie entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> Update(Specie entity)
    {
        throw new NotImplementedException();
    }

    public override Task Delete(Specie entity)
    {
        throw new NotImplementedException();
    }
    
    public override Task<Specie> Find(int Id)
    {
        throw new NotImplementedException();
    }
    
    #endregion
}