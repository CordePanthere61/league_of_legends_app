using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class StoryRepository : Repository<Story>
{

    private const string BaseSelect = "select s.id \"story.Id\", s.name \"story.Name\", s.text \"story.Text\"" +
                                      " reg.id \"region.Id\", reg.name \"region.Name\"" +
                                      " a.id \"author.Id\", a.name \"author.Name\"" +
                                      " from story s" +
                                      " join region reg on r.id = s.id_region" +
                                      " join author a on a.id = s.id_author";

    private const string BaseSelectAll = BaseSelect + " order by s.name";

    private const string BaseSelectSingle = BaseSelect + " where s.id = @id";
    
    private RegionRepository _regionRepository;
    private AuthorRepository _authorRepository;
    
    public StoryRepository()
    {
        _regionRepository = new RegionRepository();
        _authorRepository = new AuthorRepository();
    }
    
    public override Task<Story> Find(int id)
    {
        return Task.Run(() => _database.SelectSingle(BaseSelectSingle, this, new Parameter("id", id)));
    }

    public override Task<List<Story>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }

    public override Task<int> Insert(Story entity)
    {
        throw new NotImplementedException();
    }

    public override Task<int> Update(Story entity)
    {
        throw new NotImplementedException();
    }

    public override Task Delete(Story entity)
    {
        throw new NotImplementedException();
    }
    
    public override Story Handle(DataRow dr)
    {
        Story story = new Story();
        story.Id = dr.Field<int>("story.Id");
        story.Region = _regionRepository.Handle(dr);
        story.Author = _authorRepository.Handle(dr);
        story.Name = dr.Field<string>("story.Name");
        story.Text = dr.Field<string>("story.Text");
        return story;
    }
}