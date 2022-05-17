using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=database.db");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = @"
CREATE TABLE IF NOT EXISTS Computers (
    id int not null primary key,
    ram varchar(100) not null,
    processor varchar(100) not null
   );
";

CREATE TABLE IF NOT EXISTS Lab (
    id int not null primary key,
    number int not null,
    name varchar(100) not null,
    block varchar (20) not null
);
";

command.ExecuteNonQuery();

connection.Close();


// Routing

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";

        var reader = command.ExecuteReader();

        Console.WriteLine("Computer List");
        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            reader.GetInt32(0);
        }

        reader.Close();
        connection.Close();

    }

    if(modelAction == "New")
    {
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);
        command.ExecuteNonQuery();

        connection.Close();

        Console.WriteLine("New computer");
        Console.WriteLine("{0}, {1}, {2}", id, ram, processor);
    }
}

if (modelName == "Lab")
{
    if (modelAction == "List")
    {
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Lab;";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                "{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)
            );
        }

        reader.Close();
        connection.Close();
    }

    if (modelAction == "New")
    {
    
        int id= Convert.ToInt32(args[2]);
        int number = Convert.ToInt32(args[3]);
        string name = args[4];
        string block = args[5];

         connection = new SqliteConnection("Data Source=database.db");
         connection.Open();

          command = connection.CreateCommand();
          command.CommandText = "INSERT INTO Lab VALUES($id, $number, $name, $block);";
          command.Parameters.AddWithValue("$id", id);
          command.Parameters.AddWithValue("$number", number);
          command.Parameters.AddWithValue("$name", name);
          command.Parameters.AddWithValue("$block", block);
          command.ExecuteNonQuery();

           connection.Close();
    }
}