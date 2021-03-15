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
                        defectFile = file; 

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
                        enhancementFile = file; 

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
                        taskFile = file; 

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
        public void AddTicket(Ticket ticket, int type){
            if(type == 1){
                Defect defect = (Defect)ticket;
                try{
                    StreamWriter sw = new StreamWriter(defectFile, true);
                    sw.WriteLine($"{defect.id},{defect.summary},{defect.status},{defect.priority},{defect.submitter},{defect.assigned},{String.Join("|", defect.watching)},{defect.severity}");
                    sw.Close(); 
                }catch(Exception e){
                    logger.Error(e.Message);
                }
                logger.Info($"Defect {defect.id} entered"); 
            }
            else if (type == 2){
                Enhancement en =(Enhancement)ticket; 
                try{
                    StreamWriter sw = new StreamWriter(enhancementFile, true);
                    sw.WriteLine($"{en.id},{en.summary},{en.status},{en.priority},{en.submitter},{en.assigned},{String.Join("|", en.watching)},{en.software},{en.cost},{en.reason},{en.estimate}");
                    sw.Close(); 
                }catch(Exception e){
                    logger.Error(e.Message);
                }
                logger.Info($"Enhancement {en.id} entered"); 
            }
            else if (type == 3){
                Task task = (Task)ticket; 
                try{
                    StreamWriter sw = new StreamWriter(taskFile, true);
                    sw.WriteLine($"{task.id},{task.summary},{task.status},{task.priority},{task.submitter},{task.assigned},{String.Join("|", task.watching)},{task.projectName},{task.dueDate}");
                    sw.Close(); 
                }catch(Exception e){
                    logger.Error(e.Message);
                }
                logger.Info($"Task {task.id} entered"); 
            }
            
            //update Ticket list 
            Tickets.Add(ticket); 
        }

        public int getNewID(){
            return Tickets[Tickets.Count - 1].id + 1; 
        }
        //Method to ensure unique id
        // public bool IsUnique(int id){
        //     bool repeat = true; 
            
        //     foreach(Ticket ticket in Tickets){
        //         if (id == ticket.id)
        //         {
        //             Console.WriteLine($"Invalid ID. Next avaiable ID {Tickets[Tickets.Count-1].id + 1}"); 
        //             repeat = false; 
        //         }
        //     }

        //     return repeat; 
        // }
    }
}