namespace league_of_legends_app.CORE.Interfaces;

public interface IValidator<T> where T : class
{
    public bool Validate(T t);
}