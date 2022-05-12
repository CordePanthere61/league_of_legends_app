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

    private const string DeleteAllRecommendedTagsForChampion = "delete from champion_tag" +
                                                               " where id_champion = @id";

    private const string InsertRecommendTagForChampion = "insert into champion_tag(id_champion, id_tag)" +
                                                         " values (@id_champion, @id_tag)";

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

    public async Task InsertOrUpdateChampionTags(int championId, List<Tag> selectedTags)
    {
        await Task.Run(() =>
        {
            _database.Delete(DeleteAllRecommendedTagsForChampion, new Parameter("id", championId));
            foreach (Tag tag in selectedTags)
            {
                _database.Insert(InsertRecommendTagForChampion, new[]
                {
                    new Parameter("id_champion", championId),
                    new Parameter("id_tag", tag.Id)
                });
            }
        });
    }
    
    #region NotImplemented
    
    public override Task<Tag> Find(int Id)
    {
        throw new NotImplementedException();
    }
    
    public override Task<int> Insert(Tag entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> Update(Tag entity)
    {
        throw new NotImplementedException();
    }

    public override Task Delete(Tag entity)
    {
        throw new NotImplementedException();
    }
    
    #endregion
}