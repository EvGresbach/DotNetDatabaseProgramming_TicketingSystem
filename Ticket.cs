using System;
using System.Collections.Generic; 

class Ticket{
    public int id {get; set;}
    public string summary {get; set;}
    public string status {get; set;}
    public string priority {get; set;}
    public string submitter {get; set;}
    public string assigned {get; set;}
    List<string> watching {get; set;} 

}