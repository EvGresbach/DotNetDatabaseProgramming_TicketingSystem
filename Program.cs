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
            TicketFile defects = new TicketFile("Tickets.csv", "Defect"); 
            TicketFile enhancements = new TicketFile("Enhancements.csv", "Enhancement");
            TicketFile tasks = new TicketFile("Task.csv", "Task"); 

            string userChoice; 
            do {
                Console.Write("\n1. Read data from file\n2. Add data to file \nEnter any key to exit\n>");
                userChoice = Console.ReadLine(); 
                // choice 1 - read data
                if (userChoice == "1")
                {
                    //ask what file to read from 
                    Console.Write("\nWhich type of ticket do you wish to look at?\n1. Bugs/Defects\n2. Enhancements\n3. Tasks\n>"); 
                    int toReadFrom; 
                    while(!Int32.TryParse(Console.ReadLine(), out toReadFrom)){
                        Console.Write("\nAnswer must be an integer\n1. Bugs/Defects\n2. Enhancements\n3. Tasks\n>");
                    } 

                    //read from that file
                    if(toReadFrom == 1){
                        foreach(Defect defect in defects.Tickets){
                            Console.WriteLine("\n" + defect.Display()); 
                        }
                    }
                    else if(toReadFrom == 2){
                        foreach(Enhancement en in enhancements.Tickets){
                            Console.WriteLine("\n" + en.Display()); 
                        }
                    }
                    else if(toReadFrom == 3){
                        foreach(Task task in tasks.Tickets){
                            Console.WriteLine("\n" + task.Display()); 
                        }
                    }
                }
                // choice 2 - add data
                else if (userChoice == "2")
                {
                   //ask what file to write to
                   Console.Write("\nWhich type of ticket do you wish to write?\n1. Bug/Defect\n2. Enhancement\n3. Task\n>"); 
                    int toWrite; 
                    while(!Int32.TryParse(Console.ReadLine(), out toWrite)){
                        Console.Write("\nAnswer must be an integer\n1. Bugs/Defect\n2. Enhancement\n3. Task\n>");
                    } 

                   //collect information 
                   //summary, status, priority, submitter, assigned, watching
                   Console.Write("Enter Summary: "); 
                   string summary = Console.ReadLine(); 
                   Console.Write("Enter Status: "); 
                   string status = Console.ReadLine(); 
                   Console.Write("Enter Priority: "); 
                   string priority = Console.ReadLine(); 
                   Console.Write("Enter Submitter: "); 
                   string submitter = Console.ReadLine(); 
                   Console.Write("Enter Assigned: "); 
                   string assigned = Console.ReadLine();

                   List<string> watchers = new List<string>(); 
                   string watcher; 
                   do{
                       Console.Write("Enter Watching (or done to quit): ");  
                       watcher = Console.ReadLine(); 
                       if(!watcher.Equals("done")){
                           watchers.Add(watcher); 
                       }
                    } while(!watcher.Equals("done")); 

                   //defect - id, severity
                   if(toWrite == 1){
                       //id
                       Console.Write("Enter Severity: "); 
                       string severity = Console.ReadLine(); 
                   }
                   //enhancement - id, software, cost, reason, estimate, 
                   else if(toWrite == 2){
                       //id 
                       Console.WriteLine("Enter Software: "); 
                       string software = Console.ReadLine(); 
                       Console.Write("Enter Cost: "); 
                       double cost = Double.Parse(Console.ReadLine()); 
                       Console.WriteLine("Enter Reason: "); 
                       string reason = Console.ReadLine(); 
                       Console.Write("Enter Estimate: "); 
                       double estimate = Double.Parse(Console.ReadLine()); 
                   }
                   //task - id, project name, due date
                    else if(toWrite == 3){
                        //id 
                        Console.Write("Enter Project Name: "); 
                        string projectName = Console.ReadLine(); 
                        Console.WriteLine("Enter Due Date (formatting): "); 
                        DateTime dueDate = DateTime.Parse(Console.ReadLine()); 
                    }
                   //add ticket to ticketfile list 
                   
                }
            } while (userChoice == "1" || userChoice == "2");
        }
    }
}
