using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class RoleRepository : Repository<Role>
{
    private const string BaseSelectAll = "select r.id \"role.Id\", r.name \"role.Name\" from role r";

    private const string RecommendedRolesSelectAll = "select r.id \"role.Id\", r.name \"role.Name\"" +
                                                  " from role r" +
                                                  " join recommended_role rr on r.id = rr.id_role" +
                                                  " where rr.id_champion = @id";

    private const string DeleteAllRecommendedRolesForChampion = "delete from recommended_role" +
                                                                " where id_champion = @id";
    
    private const string InsertRecommendRoleForChampion = "insert into recommended_role(id_champion, id_role)" +
                                                          " values (@id_champion, @id_role)";
    
    public override Task<List<Role>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }
    
    public async Task InsertOrUpdateRecommendRoles(int championId, List<Role> selectedRoles)
    {
        await Task.Run(() =>
        {
            _database.Delete(DeleteAllRecommendedRolesForChampion, new Parameter("id", championId));
            foreach (Role role in selectedRoles)
            {
                _database.Insert(InsertRecommendRoleForChampion, new[]
                {
                    new Parameter("id_champion", championId),
                    new Parameter("id_role", role.Id)
                });
            }
        });
    }

    public Task<List<Role>> FindRecommendedRolesForChampion(int championId)
    {
        return Task.Run(() => _database.Select(RecommendedRolesSelectAll, this, new Parameter("id", championId)));
    }
    
    public override Role Handle(DataRow dr)
    {
        Role role = new Role();
        role.Id = dr.Field<int>("role.Id");
        role.Name = dr.Field<string>("role.Name");
        return role;
    }

    #region NotImplemented
    
    public override Task<int> Update(Role entity)
    {
        throw new NotImplementedException();
    }

    public override Task Delete(Role entity)
    {
        throw new NotImplementedException();
    }
    
    public override Task<int> Insert(Role entity)
    {
        throw new NotImplementedException();
    }
    
    public override Task<Role> Find(int Id)
    {
        throw new NotImplementedException();
    }
    
    #endregion
    
}