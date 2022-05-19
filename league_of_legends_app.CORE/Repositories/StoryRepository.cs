using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class StoryRepository : Repository<Story>
{

    private const string BaseSelect = "select s.id \"story.Id\", s.name \"story.Name\", s.text \"story.Text\"," +
                                      " r.id \"region.Id\", r.name \"region.Name\"," +
                                      " a.id \"author.Id\", a.name \"author.Name\"" +
                                      " from story s" +
                                      " join region r on r.id = s.id_region" +
                                      " join author a on a.id = s.id_author";

    private const string BaseSelectAll = BaseSelect + " order by s.name";

    private const string BaseSelectSingle = BaseSelect + " where s.id = @id";

    private const string BaseInsert = "insert into story(id_region, id_author, name, text)" +
                                      " values (@id_region, @id_author, @name, @text)" +
                                      " returning id";

    private const string BaseUpdate = "update story" +
                                      " set id_region = @id_region," +
                                      " id_author = @id_author," +
                                      " name = @name," +
                                      " text = @text" +
                                      " where id = @id" +
                                      " returning story.id as id";
    
    private const string BaseDelete = "delete from story" +
                                      " where id = @id";
    
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

    public override Task<int> Insert(Story story)
    {
        return Task.Run(() => _database.Insert(BaseInsert, new[]
        {
            new Parameter("id_region", story.Region.Id),
            new Parameter("id_author", story.Author.Id),
            new Parameter("name", story.Name),
            new Parameter("text", story.Text),
        })); 
    }

    public override Task<int> Update(Story story)
    {
        return Task.Run(() => _database.Update(BaseUpdate, new[]
        {
            new Parameter("id_region", story.Region.Id),
            new Parameter("id_author", story.Author.Id),
            new Parameter("name", story.Name),
            new Parameter("text", story.Text),
            new Parameter("id", story.Id),
        }));
    }

    public override Task Delete(Story story)
    {
        return Task.Run(() =>
        {
            _database.Delete(BaseDelete, new Parameter("id", story.Id));
        });
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