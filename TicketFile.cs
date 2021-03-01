using System; 
using System.Collections.Generic;
using System.IO; 

class TicketFile{
    public string file {get; set;}
    public List<Ticket> Tickets {get; set;}

    //Constructor to reads from file
    public TicketFile(string file){
        this.file = file; 
        Tickets = new List<Ticket>(); 
        StreamReader sr = new StreamReader(file);

        while(!sr.EndOfStream){
            string line = sr.ReadLine(); 
            string[] ticket = line.Split(","); 
            Ticket tempTicket = new Ticket();

            tempTicket.id = Int32.Parse(ticket[0]); 
            tempTicket.summary = ticket[1]; 
            tempTicket.status = ticket[2];
            tempTicket.priority = ticket[3];
            tempTicket.submitter = ticket[4];
            tempTicket.assigned = ticket[5];

            string[] watchers = ticket[6].Split("|");
            foreach(string s in watchers){
                tempTicket.watching.Add(s); 
            }

            Tickets.Add(tempTicket); 
        }

        sr.Close(); 
    }
    //Method to write to file
    public void AddTicket(Ticket ticket){
        StreamWriter sw = new StreamWriter(file, true);
        sw.WriteLine($"{ticket.id},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{String.Join("|", ticket.watching)}");
        sw.Close(); 

        //update Ticket list 
        Tickets.Add(ticket); 
    }

    //Method to ensure unique id
    public bool IsUnique(int id){
        bool repeat = false; 
        
        foreach(Ticket ticket in Tickets){
            if (id == ticket.id)
                repeat = true; 
        }

        return repeat; 
    }
}