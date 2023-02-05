/*
.Net Database Programming 156-101-20024-23
Briana Lindquist
A1-Ticketing System
2023-02-05
*/

namespace A1TicketSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variable to auto assign a ticket number
            int ticket = 0;

            //File that will be used to save the entries and display them to the user
            string file = "tickets.csv";

            //Tracks the menu choice chosen
            string choice;

            //Continue looping the menu until the user is done reading or creating a file
            do
            {
                Console.WriteLine("1) Read data from file.");
                Console.WriteLine("2) Create file from data.");
                Console.WriteLine("Press any other key to exit.");

                choice = Console.ReadLine();

                if (choice == "1")
                {
                    StreamReader sr = new StreamReader(file);

                    //Read in the header and throw it away, a way to ignore header rows
                    sr.ReadLine(); 

                    //Check for an empty file
                    if (sr.EndOfStream)
                    {
                        Console.WriteLine("The file is currently empty. Enter a ticket before trying to read the file.");
                        Console.WriteLine();
                    }
                    else
                    {
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            Console.WriteLine(line);
                        }

                    }
                    //Close the file when done reading it
                    sr.Close();
                }
                else if (choice == "2")
                {
                    StreamWriter sw = new StreamWriter(file);
                    
                    //Write the header information to the file before adding lines to it
                    sw.WriteLine($"TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");

                    //Variable to track whether a ticket is to be added to the file
                    string resp;

                    do
                    {
                        Console.WriteLine("Enter a ticket Y/N?");
                        resp = Console.ReadLine().ToUpper();
                        
                        if (resp == "Y")
                        {
                            //Auto increment the ticket number
                            ticket++;

                            Console.WriteLine("Enter the ticket summary.");
                            string summary = Console.ReadLine();

                            Console.WriteLine("Enter the status (High,Medium,Low).");
                            string status = Console.ReadLine();

                            Console.WriteLine("Enter the priority (1-10).");
                            string priority = Console.ReadLine();

                            Console.WriteLine("Enter the submitter person's name.");
                            string submitter = Console.ReadLine();

                            Console.WriteLine("Enter the assigned person's name.");
                            string assigned = Console.ReadLine();

                            //Create a list to track the watchers entered.
                            List<string> myWatchers = new List<string>();

                            //Variable to track whether a watcher is to be added to the ticket
                            string entry = "Y";

                            //Variable to track the watcher's name
                            string watcher;

                            //Variable to track the number of watchers entered.
                            int watcherCount = 0;

                            //Continue looping as long as the user wants to enter a watcher
                            do
                            {
                                Console.WriteLine("Enter a watcher for the ticket Y/N?");
                                entry = Console.ReadLine().ToUpper();

                                if (entry == "Y")
                                {
                                    watcherCount++;
                                    Console.WriteLine("Enter the watcher person's name.");
                                    watcher = Console.ReadLine();

                                    //If more than 1 watcher entered, add a "|" to separate them in the list
                                    if (watcherCount > 1)
                                    {
                                        watcher = "|" + watcher;
                                    }
                                    myWatchers.Add(watcher);
                                }
                            } while (entry == "Y");

                            //Write the ticket information to the file
                            sw.Write($"{ticket},{summary},{status},{priority},{submitter},{assigned},");

                            //Loop through the watcher's list to add all the watchers entered for the ticket
                            foreach (string watcherName in myWatchers)
                            {
                                sw.Write($"{watcherName}");
                            }
                            sw.WriteLine();
                        }
                    } while (resp == "Y");

                    sw.Close(); //Close the file.
                }
            } while (choice == "1" || choice == "2");
        }
    }
}