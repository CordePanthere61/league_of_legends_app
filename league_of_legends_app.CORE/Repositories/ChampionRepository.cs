using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class ChampionRepository : Repository<Champion>
{

    private const string BaseSelect = "select c.id \"champion.Id\", c.id_specie \"champion.IdSpecie\", c.id_difficulty \"champion.IdDifficulty\", c.id_region \"champion.IdRegion\", c.name \"champion.Name\", c.alias \"champion.Alias\", c.release_date \"champion.ReleaseDate\", c.price_be \"champion.PriceBe\", c.price_rp \"champion.PriceRp\", c.quote \"champion.Quote\", c.is_melee \"champion.IsMelee\"," +
                                         " s.id \"specie.Id\", s.name \"specie.Name\"," +
                                         " d.id \"difficulty.Id\", d.name \"difficulty.Name\"," +
                                         " r.id \"region.Id\", r.name \"region.Name\"" +
                                         " from champion c " +
                                         " join specie s on s.id = c.id_specie" +
                                         " join difficulty d on d.id = c.id_difficulty" +
                                         " join region r on r.id = c.id_region";

    private const string BaseInsert = "insert into champion(id_specie, id_difficulty, id_region, name, alias, release_date, price_be, price_rp, quote, is_melee)" +
                                      " values (@id_specie, @id_difficulty, @id_region, @name, @alias, @release_date, @price_be, @price_rp, @quote, @is_melee)" +
                                      " returning id";

    private const string BaseUpdate = "update champion" +
                                      " set id_specie = @id_specie," +
                                      " id_difficulty = @id_difficulty," +
                                      " id_region = @id_region," +
                                      " name = @name," +
                                      " alias = @alias," +
                                      " release_date = @release_date," +
                                      " price_be = @price_be," +
                                      " price_rp = @price_rp," +
                                      " quote = @quote," +
                                      " is_melee = @is_melee" +
                                      " where id = @id" +
                                      " returning champion.id as id";

    private const string BaseDelete = "delete from champion" +
                                      " where id = @id";

    private const string SelectStoryChampions = "select c.id \"champion.Id\", c.name \"champion.Name\"" +
                                                " from champion c" +
                                                " join champion_story cs" +
                                                " where cs.id_story = @id";

    private const string BaseSelectSingle = BaseSelect + " where c.id = @id";

    private const string BaseSelectAll = BaseSelect + " order by c.name";


    private SpecieRepository _specieRepository;
    private DifficultyRepository _difficultyRepository;
    private RegionRepository _regionRepository;

    public ChampionRepository()
    {
        _specieRepository = new SpecieRepository();
        _difficultyRepository = new DifficultyRepository();
        _regionRepository = new RegionRepository();
    }
    
    public override Task<Champion> Find(int id)
    {
        return Task.Run(() => _database.SelectSingle(BaseSelectSingle, this, new Parameter("id", id)));
    }

    public override Task<List<Champion>> FindAll()
    {
        return Task.Run(() => _database.Select(BaseSelectAll,this));
    }

    public Task<List<Champion>> FindAssociatedChampionsToStory(int storyId)
    {
        return Task.Run((() => _database.Select(SelectStoryChampions, this, new Parameter("id", storyId))));
    }

    public override Task<int> Insert(Champion champion)
    {
        return Task.Run(() => _database.Insert(BaseInsert, new[]
        {
            new Parameter("id_specie", champion.Specie.Id),
            new Parameter("id_difficulty", champion.Difficulty.Id),
            new Parameter("id_region", champion.Region.Id),
            new Parameter("name", champion.Name!),
            new Parameter("alias", champion.Alias!),
            new Parameter("release_date", champion.ReleaseDate!),
            new Parameter("price_be", champion.PriceBe),
            new Parameter("price_rp", champion.PriceRp),
            new Parameter("quote", champion.Quote!),
            new Parameter("is_melee", champion.IsMelee)
        }));
    }

    public override Task<int> Update(Champion champion)
    {
        return Task.Run(() => _database.Update(BaseUpdate, new[]
        {
            new Parameter("id_specie", champion.Specie.Id),
            new Parameter("id_difficulty", champion.Difficulty.Id),
            new Parameter("id_region", champion.Region.Id),
            new Parameter("name", champion.Name!),
            new Parameter("alias", champion.Alias!),
            new Parameter("release_date", champion.ReleaseDate!),
            new Parameter("price_be", champion.PriceBe),
            new Parameter("price_rp", champion.PriceRp),
            new Parameter("quote", champion.Quote!),
            new Parameter("is_melee", champion.IsMelee),
            new Parameter("id", champion.Id)
        }));
    }

    public override async Task Delete(Champion champion)
    {
        await Task.Run(() =>
        {
            _database.Delete(BaseDelete, new Parameter("id", champion.Id));
        });
    }

    public override Champion Handle(DataRow dr)
    {
        Champion champion = new Champion();
        champion.Id = dr.Field<int>("champion.Id");
        champion.Specie = _specieRepository.Handle(dr);
        champion.Difficulty = _difficultyRepository.Handle(dr);
        champion.Region = _regionRepository.Handle(dr);
        champion.Name = dr.Field<string>("champion.Name");
        champion.Alias = dr.Field<string>("champion.Alias");
        champion.ReleaseDate = dr.Field<DateTime>("champion.ReleaseDate").Date;
        champion.PriceBe = dr.Field<int>("champion.PriceBe");
        champion.PriceRp = dr.Field<int>("champion.PriceRp");
        champion.Quote = dr.Field<string>("champion.Quote");
        champion.IsMelee = dr.Field<bool>("champion.IsMelee");
        return champion;
    }
}