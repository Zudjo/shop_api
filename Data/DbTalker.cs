using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace shop_api.Data;

public class DbTalker
{
    private MySqlConnection Connection { get; }
    private string ConnectionString = @"
        Server=127.0.0.1;
        User ID=root;
        Password=;
        Port=3306;
        Database=shop_api";

    public DbTalker()
    {
        Connection = new MySqlConnection(ConnectionString);
    }

    public List<object> SerializeMySqlDataReader(MySqlDataReader data)
    {
        List<object> objects = new List<object>();

        while (data.Read())
        {
            IDictionary<string, object> record = new Dictionary<string, object>();
            for (int i = 0; i < data.FieldCount; i++)
            {
                record.Add(data.GetName(i), data[i]);
            }
            objects.Add(record);
        }
        return objects;

    }

    public List<object> ExecuteReader(string query)
    {
        MySqlCommand cmd = new MySqlCommand(query, Connection);
        
        Connection.Open();

        MySqlDataReader data = cmd.ExecuteReader();
        List<object> result = SerializeMySqlDataReader(data);

        Connection.Close();
        return result;
    }

    public int ExecuteNonQuery(string query)
    {
        MySqlCommand cmd = new MySqlCommand(query, Connection);
        
        Connection.Open();

        int affectedRows = cmd.ExecuteNonQuery();

        Connection.Close();
        return affectedRows;
    }

}
