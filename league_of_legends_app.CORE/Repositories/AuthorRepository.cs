using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class AuthorRepository : Repository<Author>
{
    private const string BaseSelectAll = "select a.id \"author.Id\", a.name \"author.Name\" from author a";
    

    public override Task<Author> Find(int Id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Author>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }

    public override Task<int> Insert(Author entity)
    {
        return Task.Run(() => _database.Insert())
    }

    public override Task<int> Update(Author entity)
    {
        throw new NotImplementedException();
    }

    public override Task Delete(Author entity)
    {
        throw new NotImplementedException();
    }
    
    public override Author Handle(DataRow dr)
    {
        Author author = new Author();
        author.Id = dr.Field<int>("author.Id");
        author.Name = dr.Field<string>("author.Name");
        return author;
    }
}