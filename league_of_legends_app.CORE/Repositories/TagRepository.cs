using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class TagRepository : Repository<Tag>
{
    private const string BaseSelectAll = "select t.id \"tag.Id\", t.name \"tag.Name\" from tag t";

    public override Tag Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Tag>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll, this));
    }

    public override Tag Handle(DataRow dr)
    {
        Tag tag = new Tag();
        tag.Id = dr.Field<int>("tag.Id");
        tag.Name = dr.Field<string>("tag.Name");
        return tag;
    }
}