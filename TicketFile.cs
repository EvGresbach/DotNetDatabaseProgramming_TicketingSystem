using System; 
using System.Collections.Generic;
using System.IO; 

class TicketFile{
    public string file {get; set;}
    public List<Ticket> Tickets {get; set;}

    //Constructor to reads from file
    public TicketFile(string file){
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
        }
    }
    //Method to write to file
    //Method to get unique id
}