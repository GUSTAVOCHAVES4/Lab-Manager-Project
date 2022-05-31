using LabManager.Database;
using LabManager.Models;
using LabManager.Repositories;
using Microsoft.Data.Sqlite;


var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

// Routing

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
       Console.WriteLine("Computer List");
       foreach(var computer in computerRepository.GetAll())
       {
           Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
       }

    }

    if(modelAction == "New")
    {
        Console.WriteLine("Computer New");
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);

    }

    if (modelAction == "Show")
    {
        int id = Convert.ToInt32(args[2]);
        var computer = computerRepository.GetById(id);
        Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
    }

    if (modelAction == "Update")
    {
        Console.WriteLine("Computer Updated");
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Update(computer);
    }

    if (modelAction == "Delete")
    {
        Console.WriteLine("Computer Deleted");
        int id = Convert.ToInt32(args[2]);
        computerRepository.Delete(id);
    }
}   


