using System; 
using System.Collections.Generic;
using System.IO; 
using NLog.Web; 

namespace TicketingSystem{
    class TicketFile{
        public string defectFile {get; set;}
        public string enhancementFile {get; set;}
        public string taskFile{get; set;}
        public List<Ticket> Tickets {get; set;}
        
        public NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        //Constructor to reads from file
        public TicketFile(string file, string type){
            Tickets = new List<Ticket>(); 

            //switch for each type of ticket
            switch(type){
                case "Defect":
                    try{
                        StreamReader sr = new StreamReader(file);
                        

                        while(!sr.EndOfStream){
                            string line = sr.ReadLine(); 
                            string[] ticket = line.Split(","); 
                            Defect defect = new Defect();

                            defect.id = Int32.Parse(ticket[0]); 
                            defect.summary = ticket[1]; 
                            defect.status = ticket[2];
                            defect.priority = ticket[3];
                            defect.submitter = ticket[4];
                            defect.assigned = ticket[5];
                            defect.severity = ticket[7]; 

                            string[] watchers = ticket[6].Split("|");
                            foreach(string s in watchers){
                                defect.watching.Add(s); 
                            }

                            Tickets.Add(defect); 
                        }

                        sr.Close(); 
                    } catch(Exception e){
                        logger.Error(e.Message); 
                    }
                     
                    break; 
                case "Enhancement":
                    try{
                        StreamReader sr = new StreamReader(file);
                        

                        while(!sr.EndOfStream){
                            string line = sr.ReadLine(); 
                            string[] ticket = line.Split(","); 
                            Enhancement enhancement = new Enhancement(); 

                            enhancement.id = Int32.Parse(ticket[0]); 
                            enhancement.summary = ticket[1]; 
                            enhancement.status = ticket[2];
                            enhancement.priority = ticket[3];
                            enhancement.submitter = ticket[4];
                            enhancement.assigned = ticket[5];
                            enhancement.software = ticket[7]; 
                            enhancement.cost = Double.Parse(ticket[8]); 
                            enhancement.reason = ticket[9];
                            enhancement.estimate = Double.Parse(ticket[10]); 

                            string[] watchers = ticket[6].Split("|");
                            foreach(string s in watchers){
                                enhancement.watching.Add(s); 
                            }

                            Tickets.Add(enhancement); 
                        }

                        sr.Close(); 
                    } catch(Exception e){
                        logger.Error(e.Message); 
                    } 
                    break;
                case "Task":
                    try{
                        StreamReader sr = new StreamReader(file);
                        

                        while(!sr.EndOfStream){
                            string line = sr.ReadLine(); 
                            string[] ticket = line.Split(","); 
                            Task task = new Task(); 

                            task.id = Int32.Parse(ticket[0]); 
                            task.summary = ticket[1]; 
                            task.status = ticket[2];
                            task.priority = ticket[3];
                            task.submitter = ticket[4];
                            task.assigned = ticket[5];
                            task.projectName = ticket[7]; 
                            task.dueDate = DateTime.Parse(ticket[8]); 

                            string[] watchers = ticket[6].Split("|");
                            foreach(string s in watchers){
                                task.watching.Add(s); 
                            }

                            Tickets.Add(task); 
                        }

                        sr.Close(); 
                    } catch(Exception e){
                        logger.Error(e.Message); 
                    } 
                    break; 
            }
            
        }
        //Method to write to file
        public void AddTicket(Ticket ticket){
            try{
                StreamWriter sw = new StreamWriter(file, true);
                sw.WriteLine($"{ticket.id},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{String.Join("|", ticket.watching)}");
                sw.Close(); 
            }catch(Exception e){
                logger.Error(e.Message);
            }
            logger.Info($"{ticket.id},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{String.Join("|", ticket.watching)} entered"); 
            //update Ticket list 
            Tickets.Add(ticket); 
        }

        //Method to ensure unique id
        public bool IsUnique(int id){
            bool repeat = true; 
            
            foreach(Ticket ticket in Tickets){
                if (id == ticket.id)
                {
                    Console.WriteLine($"Invalid ID. Next avaiable ID {Tickets[Tickets.Count-1].id + 1}"); 
                    repeat = false; 
                }
            }

            return repeat; 
        }
    }
}