using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public List<Computer> GetAll()

    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";

        var reader = command.ExecuteReader();

        var computers = new List<Computer>();

        while(reader.Read()) //quando não sei quantos itens tenho
        {
            //var computer = readerToComputer(reader);
            //computers.Add(computer); outro meio de fazer oq está abaixo

            computers.Add(readerToComputer(reader));
        }
        connection.Close();

        return computers;
    }

    public Computer Save (Computer computer)
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);
        
        command.ExecuteNonQuery();
        connection.Close();

        return computer;

    }

    public Computer GetById(int id)
    {
        // var id = Convert.ToInt32(args[]);
        // var computer = new Computer(id, "FAKE1", "FAKE2");

        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();

        // command.CommandText = "SELECT * FROM Computers WHERE id = $id";
        command.CommandText = "SELECT id, ram, processor FROM Computers WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader(); //ExecuteScalar() usar, fazer conversão para inteiro, vai ter um valor 0 ou 1 e devolver true ou false 
        reader.Read(); //colocar reader.Read(); pq o cursor aponta pra fora da primeira linha, aí esse comando trás pra próxima linha

        return readerToComputer(reader);


    }

    public Computer Update(Computer computer)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = //@ pula as linhas pra ficar bonitinho
        "UPDATE Computers SET ram = $ram, processor = $processor WHERE id = $id;";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();

        return computer;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery(); //executa mas não traz informação

        connection.Close();
    }

    public bool existsById(int id)
    {



        return true;
    }

    private Computer readerToComputer(SqliteDataReader reader)
    {
        return new Computer (reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
    }
}

