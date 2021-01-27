using System;
using System.IO; 
using System.Collections.Generic;

namespace TicketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "Tickets.csv";
            string userChoice; 
            do {
                Console.WriteLine("1. Read data from file\n2. Add data to file \nEnter any key to exit");
                userChoice = Console.ReadLine(); 
                // choice 1 - read data
                if (userChoice == "1")
                {
                    if (File.Exists(file))
                    {
                        StreamReader sr = new StreamReader(file);
                        
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] ticket = line.Split(',');
                            string[] watcher = ticket[6].Split('|');
                            Console.WriteLine($"Ticket {ticket[0]} \n----------------------------");
                            Console.WriteLine($"Summary: {ticket[1]}\nStatus: {ticket[2]} \nPriority: {ticket[3]}\n"
                                + $"Submitter: {ticket[4]} \nAssigned: {ticket[5]} \nWatching: {String.Join(", ", watcher)}\n\n");
                        }
                        sr.Close(); 
                    }
                    else Console.WriteLine("This file does not exist");
                }
                // choice 2 - add data
                else if (userChoice == "2")
                {
                    StreamWriter sw = new StreamWriter(file);

                    Console.Write("Enter Ticket ID: "); 
                    string ticketID = Console.ReadLine(); 
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
                    
                    string moreWatching;
                    List<string> watching = new List<string>(); 

                    do {
                        Console.Write("Enter Watching: "); 
                        watching.Add(Console.ReadLine()); 

                        Console.Write("Enter another person watching? Y/N: ");
                        moreWatching = Console.ReadLine().ToUpper(); 
                    } while(moreWatching == "Y");

                    sw.WriteLine($"{ticketID},{summary},{status},{priority},{submitter},{assigned},{String.Join("|", watching)}"); 
                    sw.Close();
                }
            } while (userChoice == "1" || userChoice == "2");
        }
    }
}
