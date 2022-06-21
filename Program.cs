using LabManager.Database;
using LabManager.Models;
using LabManager.Repositories;

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
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        } 
        else 
        {
            Console.WriteLine($"O computador com Id {id} não existe.");
    }

    if (modelAction == "Update")
    {
        var Id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
            string ram = args[3];
            string processor = args[4];
            var computer = new Computer(id, ram, processor);
            computerRepository.Update(computer);
        }
        else
        {
            Console.WriteLine($"O computador com Id {id} não existe.");
        }
    }

    if (modelAction == "Delete")
    {
        var Id = Convert.ToInt32(args[2]);
        Console.WriteLine("Computer Deleted");

        if(computerRepository.ExistsById(id))
        {
            computerRepository.Delete(id);
        }
        else
        {
            Console.WriteLine($"Computer com id {id} nao existe");
        }
    }
}
}   


