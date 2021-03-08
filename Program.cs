using System;
using System.IO; 
using System.Collections.Generic;
using NLog.Web; 
namespace TicketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
    
            // create ticket files for each type
            TicketFile defectives = new TicketFile("Tickets.csv", "Defect"); 
            TicketFile enhancements = new TicketFile("Enhancements.csv", "Enhancement");
            TicketFile tasks = new TicketFile("Task.csv", "Task"); 


            string userChoice; 
            do {
                Console.Write("1. Read data from file\n2. Add data to file \nEnter any key to exit\n>");
                userChoice = Console.ReadLine(); 
                // choice 1 - read data
                if (userChoice == "1")
                {
                    //ask what file to read from 
                    Console.Write("Which type of ticket do you wish to look at?\n1. Bugs/Defects\n2. Enhancements\n3. Tasks\n>"); 
                    int toReadFrom; 
                    while(!Int32.TryParse(Console.ReadLine(), out toReadFrom)){
                        Console.Write("Answer must be an integer\n1. Bugs/Defects\n2. Enhancements\n3. Tasks\n>");
                    } 

                    //read from that file
                    
                    
                }
                // choice 2 - add data
                else if (userChoice == "2")
                {
                   //ask what file to write to
                   Console.Write("Which type of ticket do you wish to write?\n1. Bug/Defect\n2. Enhancement\n3. Task\n>"); 
                    int toWrite; 
                    while(!Int32.TryParse(Console.ReadLine(), out toWrite){
                        Console.Write("Answer must be an integer\n1. Bugs/Defect\n2. Enhancement\n3. Task\n>");
                    } 

                   //collect information 
                   //add ticket to ticketfile list 
                }
            } while (userChoice == "1" || userChoice == "2");
        }
    }
}
