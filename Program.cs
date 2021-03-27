using System;
using System.IO; 
using System.Collections.Generic;
using NLog.Web; 
using System.Linq; 
namespace TicketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

            logger.Info("Program Started");

            // create ticket files for each type
            TicketFile defects = new TicketFile("Tickets.csv", "Defect"); 
            TicketFile enhancements = new TicketFile("Enhancements.csv", "Enhancement");
            TicketFile tasks = new TicketFile("Task.csv", "Task"); 

            string userChoice; 
            do {
                Console.Write("\n1. Read data from file\n2. Add data to file\n3. Search for ticket\nEnter any key to exit\n>");
                userChoice = Console.ReadLine(); 
                // choice 1 - read data
                if (userChoice == "1")
                {
                    //ask what file to read from 
                    Console.Write("\nWhich type of ticket do you wish to look at?\n1. Bugs/Defects\n2. Enhancements\n3. Tasks\n>"); 
                    int readFrom; 
                    while(!Int32.TryParse(Console.ReadLine(), out readFrom)){
                        Console.Write("\nAnswer must be an integer\n1. Bugs/Defects\n2. Enhancements\n3. Tasks\n>");
                    } 

                    //read from that file
                    if(readFrom == 1){
                        foreach(Defect defect in defects.Tickets){
                            Console.WriteLine("\n" + defect.Display()); 
                        }

                        logger.Info("Defects Displayed"); 
                    }
                    else if(readFrom == 2){
                        foreach(Enhancement en in enhancements.Tickets){
                            Console.WriteLine("\n" + en.Display()); 
                        }

                        logger.Info("Enhancements Displayed"); 
                    }
                    else if(readFrom == 3){
                        foreach(Task task in tasks.Tickets){
                            Console.WriteLine("\n" + task.Display()); 
                        }

                        logger.Info("Tasks Displayed"); 
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

                   //defect - id, severity, create the new ticket
                   if(toWrite == 1){
                       //create ticket
                       Defect defect = new Defect(); 
                       // add id + severity 
                       defect.id = defects.getNewID(); 
                       Console.Write("Enter Severity: "); 
                       defect.severity = Console.ReadLine(); 
                       //add everything else
                       defect.summary = summary; 
                       defect.status = status; 
                       defect.priority = priority; 
                       defect.submitter = submitter; 
                       defect.assigned = assigned; 
                       defect.watching = watchers; 

                       defects.AddTicket(defect, toWrite); 
                   }
                   //enhancement - id, software, cost, reason, estimate, create the new ticket
                   else if(toWrite == 2){
                       //create ticket
                       Enhancement en = new Enhancement();
                       //add id, software, cost, reason, estimate
                       en.id = enhancements.getNewID(); 
                       Console.Write("Enter Software: "); 
                       en.software = Console.ReadLine(); 
                       Console.Write("Enter Cost: "); 
                       en.cost = Double.Parse(Console.ReadLine()); 
                       Console.Write("Enter Reason: "); 
                       en.reason = Console.ReadLine(); 
                       Console.Write("Enter Estimate: "); 
                       en.estimate = Double.Parse(Console.ReadLine()); 
                       //add everything else
                       en.summary = summary; 
                       en.status = status; 
                       en.priority = priority; 
                       en.submitter = submitter; 
                       en.assigned = assigned; 
                       en.watching = watchers; 

                       enhancements.AddTicket(en, toWrite);
                   }
                   //task - id, project name, due date, create the new ticket
                    else if(toWrite == 3){
                        //create ticket
                        Task task = new Task(); 
                        //add id, project name, due date 
                        task.id = tasks.getNewID(); 
                        Console.Write("Enter Project Name: "); 
                        task.projectName = Console.ReadLine(); 
                        Console.Write("Enter Due Date (mm/dd/yyyy): "); 
                        DateTime dueDate; 
                        while(!DateTime.TryParse(Console.ReadLine(), out dueDate)){
                            logger.Info("Invalid date");
                            Console.Write("Plese enter date(mm/dd/yyyy): ");
                        }
                        task.dueDate = dueDate; 

                        //add everything else
                        task.summary = summary; 
                        task.status = status; 
                        task.priority = priority; 
                        task.submitter = submitter; 
                        task.assigned = assigned; 
                        task.watching = watchers; 

                        tasks.AddTicket(task, toWrite);
                    }
                }
                else if (userChoice == "3"){
                    //status, priority, or submitter
                    Console.Write("Search by:\n1. Status\n2. Priority\n3. Submitter\n>"); 
                    int searchBy; 
                    Int32.TryParse(Console.ReadLine(), out searchBy);
                    //search term
                    Console.Write("Search For\n>");
                    string searchTerm = Console.ReadLine(); 
                    //search
                    List<Ticket> ticketsBySearch = new List<Ticket>(); 
                    switch(searchBy){
                        case 1: 
                        //status
                            ticketsBySearch.AddRange(defects.Tickets.Where(t => t.status.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList());
                            ticketsBySearch.AddRange(enhancements.Tickets.Where(t => t.status.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList());
                            ticketsBySearch.AddRange(tasks.Tickets.Where(t => t.status.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList()); 
                            break; 
                        case 2: 
                        //priority
                            break ;
                        case 3: 
                        //submitter
                            break;
                    }
                    //result
                    logger.Info($"{ticketsBySearch.Count()} tickets with {searchTerm} search term");
                    Console.Write($"The {ticketsBySearch.Count()} tickets with {searchTerm} search term:");
                    foreach(Ticket ticket in ticketsBySearch){
                        Console.WriteLine("\n" + ticket.Display()); 
                    } 
                }
            } while (userChoice == "1" || userChoice == "2" || userChoice == "3");

            logger.Info("Program Ended"); 
        }
    }
}
