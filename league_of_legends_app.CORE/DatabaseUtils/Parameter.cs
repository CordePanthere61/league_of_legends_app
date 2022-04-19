namespace league_of_legends_app.CORE.DatabaseUtils
{
    //Cette classe existe seulement pour abstraire l'existence de NPGSQLPARAMETER
    //Ce sont des paramètres BD qui seront transformés selon la classe Database
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
