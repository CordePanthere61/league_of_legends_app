using System.Reflection;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;

namespace league_of_legends_app.CORE.Validators;

public class ChampionValidator : IValidator<Champion>
{
    public bool Validate(Champion champion)
    {
        foreach (PropertyInfo prop in champion.GetType().GetProperties())
        {
            if (prop.Name != "Id" && prop.GetValue(champion) == null)
            {
                return false;
            }
        }
        return true;
    }
}