using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class RegionRepository : Repository<Region>
{
    private const string BaseSelectAll = "select r.id \"region.Id\", r.name \"region.Name\" from region r";
    

    public override Task<List<Region>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }

    public override Region Handle(DataRow dr)
    {
        Region region = new Region();
        region.Id = dr.Field<int>("region.Id");
        region.Name = dr.Field<string>("region.Name");
        return region;
    }

    #region NotImplemented

    public override Task<int> Insert(Region entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> Update(Region entity)
    {
        throw new NotImplementedException();
    }

    public override Task Delete(Region entity)
    {
        throw new NotImplementedException();
    }
    
    public override Task<Region> Find(int Id)
    {
        throw new NotImplementedException();
    }

    #endregion
}