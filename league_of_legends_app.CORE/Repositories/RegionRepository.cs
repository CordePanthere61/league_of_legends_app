using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class RegionRepository : Repository<Region>
{
    
    public override Region Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Region>> FindAll()
    {
        throw new NotImplementedException();
    }
    
    public override Region Handle(DataRow dr)
    {
        Region region = new Region();
        region.Id = dr.Field<int>("region.Id");
        region.Name = dr.Field<string>("region.Name");
        return region;
    }
}