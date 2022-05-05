using System.Data;
using league_of_legends_app.CORE.DatabaseUtils;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Repositories;

public class ChampionRepository : Repository<Champion>
{

    private const string BaseSelectAll = "select c.id \"champion.Id\", c.id_specie \"champion.IdSpecie\", c.id_difficulty \"champion.IdDifficulty\", c.id_region \"champion.IdRegion\", c.name \"champion.Name\", c.alias \"champion.Alias\", c.release_date \"champion.ReleaseDate\", c.price_be \"champion.PriceBe\", c.price_rp \"champion.PriceRp\", c.quote \"champion.Quote\", c.is_melee \"champion.IsMelee\"," +
                                         " s.id \"specie.Id\", s.name \"specie.Name\"," +
                                         " d.id \"difficulty.Id\", d.name \"difficulty.Name\"," +
                                         " r.id \"region.Id\", r.name \"region.Name\"" +
                                         " from champion c " +
                                         " join specie s on s.id = c.id_specie" +
                                         " join difficulty d on d.id = c.id_difficulty" +
                                         " join region r on r.id = c.id_region";

    private const string BaseSelectSingle = BaseSelectAll + " where c.id = @id";


    private SpecieRepository _specieRepository;
    private DifficultyRepository _difficultyRepository;
    private RegionRepository _regionRepository;

    public ChampionRepository() : base()
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