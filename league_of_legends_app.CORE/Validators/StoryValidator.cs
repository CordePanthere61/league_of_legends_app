using System.Reflection;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;

namespace league_of_legends_app.CORE.Validators;

public class StoryValidator : IValidator<Story>
{
    public bool Validate(Story story)
    {
        foreach (PropertyInfo prop in story.GetType().GetProperties())
        {
            if (prop.Name != "Id" && prop.GetValue(story) == null)
            {
                return false;
            }
        }
        return true;
    }
}