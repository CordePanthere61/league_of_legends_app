using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class TagRepository : Repository<Tag>
{
    private const string BaseSelectAll = "select t.id \"tag.Id\", t.name \"tag.Name\" from tag t";

    private const string ChampionTagSelectAll = "select t.id \"tag.Id\", t.name \"tag.Name\"" +
                                                " from tag t" +
                                                " join champion_tag ct on t.id = ct.id_tag" +
                                                " where ct.id_champion = @id";

    public override Task<Tag> Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Tag>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll, this));
    }

    public Task<List<Tag>> FindChampionTagsForChampion(int championId)
    {
        return Task.Run(() => _database.Select(ChampionTagSelectAll, this, new Parameter("id", championId)));
    }

    public override Tag Handle(DataRow dr)
    {
        Tag tag = new Tag();
        tag.Id = dr.Field<int>("tag.Id");
        tag.Name = dr.Field<string>("tag.Name");
        return tag;
    }
}