using LabManager.Database;
using LabManager.Models;
using LabManager.Repositories;
using Microsoft.Data.Sqlite;


var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);
var LabRepository = new LabRepository(databaseConfig);

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
    //Colocar os IFs relacionados aos métodos, criados no Repository, aqui no Program 

    if(modelName == "Lab")
    {
        if(modelAction == "List")
        {
            Console.WriteLine("Lab List");
            foreach(var lab in LabRepository.GetAll())
            {
                var message = $"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}";
                Console.WriteLine(message);
            }

        }

        if(modelAction == "New")
        {

        }
    } 
}

