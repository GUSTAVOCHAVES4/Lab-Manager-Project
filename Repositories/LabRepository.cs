using Dapper;
using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class LabRepository
{
    private DatabaseConfig databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public List<Lab> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); // colocando o using no começo do var, não precisa colocar connection.Close();
        connection.Open();
        List<Lab> labs = connection.Query<Lab>("SELECT * FROM Labs;").ToList();
        return labs; // quando tiver using no começo dá pra fazer: return connection.Query<Lab>("SELECT * FROM Labs;").ToList();
    }

    public Lab Save (Lab lab)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO Labs VALUES(@id, @number, @name, @block);", lab);
        return lab;
        

    }
}