using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class RoleRepository : Repository<Role>
{
    private const string BaseSelectAll = "select r.id \"role.Id\", r.name \"role.Name\" from role r";

    public override Role Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Role>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }
    
    public override Role Handle(DataRow dr)
    {
        Role role = new Role();
        role.Id = dr.Field<int>("role.Id");
        role.Name = dr.Field<string>("role.Name");
        return role;
    }
    
}