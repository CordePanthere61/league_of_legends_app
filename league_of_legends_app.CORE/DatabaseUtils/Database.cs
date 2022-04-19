using System.Configuration;
using System.Data;
using Npgsql;

namespace league_of_legends_app.CORE.DatabaseUtils;

public class Database
{
        private NpgsqlConnection connection;
        public Database(NpgsqlConnection conn)
        {
            this.connection = conn;
        }

        public static Database Create()
        {
            string connString = GetConnectionString();
            NpgsqlConnection conn = null;
            try
            {
                conn = new NpgsqlConnection(connString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return new Database(conn);
        }

        public static String GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
                return false;
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public List<TResult> Select<TResult>(String query, Repository<TResult> handler, params Parameter[] parameters)
            where TResult : class
        {
            List<TResult> results = new List<TResult>();
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();

                NpgsqlCommand cmd = CreateCommand(query, parameters);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

            }
            catch (Exception ex)
            {
                //Do something
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            foreach (DataRow dr in dt.Rows)
            {
                results.Add(handler.Handle(dr));
            }

            return results;
        }

        public TResult SelectSingle<TResult>(String query, Repository<TResult> handler, params Parameter[] parameters)
            where TResult : class
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();

                NpgsqlCommand cmd = CreateCommand(query, parameters);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

            }
            catch (Exception ex)
            {
                //Do something
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            if (dt.Rows.Count > 0)
            {
                return handler.Handle(dt.Rows[0]);
            }

            return null;
        }

        private NpgsqlCommand CreateCommand(string sql, Parameter[] parameters)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            foreach(Parameter param in parameters)
            {
                NpgsqlParameter p = new NpgsqlParameter(param.Name, param.Value);
                cmd.Parameters.Add(p);
            }

            return cmd;
        }
}
