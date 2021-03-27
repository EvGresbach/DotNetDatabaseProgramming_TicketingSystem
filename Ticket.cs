using System;
using System.Collections.Generic; 

namespace TicketingSystem{
    public abstract class Ticket{  
        public int id {get; set;}
        public string summary {get; set;}
        public string status {get; set;}
        public string priority {get; set;}
        public string submitter {get; set;}
        public string assigned {get; set;}
        public List<string> watching {get; set;} 

        //constructor
        // public Ticket(int id, string summary, string status, string priority, string submitter, string assigned, List<String> watching){
        //     this.id = id; 
        //     this.summary = summary; 
        //     this.status = status; 
        //     this.priority = priority; 
        //     this.submitter = submitter; 
        //     this.assigned = assigned; 
        //     this.watching = watching; 
        // }

        public Ticket(){
            watching = new List<string>(); 
        }

        //method to print 
        public virtual string Display(){
            return String.Format($"Ticket ID #{id}:\nSummary: {summary}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {String.Join(", ", watching)}"); 
        }
    } 

    public class Defect : Ticket {
        public string severity {get; set;}
        
        public override string Display(){
            return base.Display() + String.Format($"\nSeverity {severity}"); 
        }
    }

    public class Enhancement : Ticket {
        public string software {get; set;}
        public double cost {get; set;}
        public string reason {get; set;}
        public double estimate {get; set;}

        public override string Display()
        {
            return base.Display() + String.Format($"\nSoftware: {software}\nCost: ${cost:.00}\nReason: {reason}\nEstimate: ${estimate:.00}");
        }
    }

    public class Task : Ticket {
        public string projectName {get; set; }
        public DateTime dueDate {get; set; }

        public override string Display()
        {
            return base.Display() + String.Format($"\nProject Name: {projectName}\nDue Date: {dueDate:MM/dd/yyyy}");
        }
    }
}


   