using System;


public class Contractor
{
    public string Name
        { get; set; }
    public int Number // Phone number? Identifying number?
        {  get; set; }
    public DateOnly StartDate
        { get; set; }
    
    // Constructor for Contractor (not really needed for the project but whatever)
    public Contractor(string name, int number, DateOnly startDate)
    {
        Name = name;
        Number = number;
        StartDate = startDate;
    }
}

public class Subcontractor : Contractor
{
    public bool Shift
        { get; set; }
    public double HourlyRate
        { get; set; }

    // Constructor for Subcontractor (actually needed)
    public Subcontractor(string name, int number, DateOnly startDate, bool shift, double hourlyRate)
        : base(name, number, startDate)
    {
        Shift = shift; // false for day shift, true for night shift
        HourlyRate = hourlyRate;
    }
}

public class Program
{
    public static List<Subcontractor> subcontractors = new List<Subcontractor>();

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n----------Subcontractor Management----------");
            Console.WriteLine("" +
                "\n1. Display list" +
                "\n2. Add to list" +
                "\n3. Delete from list" +
                "\n4. Modify from list" +
                "\n5. Compute pay for contractors in list" +
                "\n6. Exit program");
            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": DisplaySubcontractors(subcontractors); break;
                case "2": AddSubcontractor(subcontractors); break;
                case "3": DeleteSubcontractor(subcontractors); break;
                case "4": ModifySubcontractor(subcontractors); break;
                case "5": ComputePay(subcontractors); break;
                case "6": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void DisplaySubcontractors(List<Subcontractor> list)
    {
        if (list.Count == 0) { Console.WriteLine("List is empty."); return; }

        // Displaying in a table format
        // Header
        Console.WriteLine($"\n{"ID",-5} | {"Name",-20} | {"Start Date",-12} | {"Shift",-10} | {"Rate",10}");
        Console.WriteLine(new string('-', 85));
        // Iterate through list and print rows
        for (int i = 0; i < list.Count; i++)
        {
            var sub = list[i];
            string shiftText = sub.Shift ? "Night" : "Day"; // Ternary conditional operator for shorthand, 1 is night, 0 is day
            Console.WriteLine($"{sub.Number,-5} | {sub.Name,-20} | {sub.StartDate,-12:d} | {shiftText,-10} | {sub.HourlyRate,10:C}"); // Print dates and rates in proper format
        }
        // Footer
        Console.WriteLine(new string('-', 85) + "\n");
    }

    static void AddSubcontractor(List<Subcontractor> list)
    {
        Console.WriteLine("Case 2 AddSubcontractor()");
        list.Add(new Subcontractor("Tom Smith", 1, new DateOnly(2000, 1, 1), false, 20 ));
    }

    static void DeleteSubcontractor(List<Subcontractor> list)
    {
        Console.WriteLine("Case 3 DeleteSubcontractor()");
    }

    static void ModifySubcontractor(List<Subcontractor> list)
    {
        int i = 0;
        int toModify;
        string modify;

        Console.WriteLine("What is the ID number of the Contractor you would like to modify?");
        Console.Write("> ");
        toModify = int.Parse(Console.ReadLine()); //gets Id number of the contractor that is to be modified
        while(i < list.Count){
            if(list[i].Number == toModify){
                Console.WriteLine("What would you like to modify?");
                Console.WriteLine("Name | Start Date | Shift Time | Hourly Rate"); 
                Console.Write("> ");
                modify = Console.ReadLine().ToLower(); //takes the input making it lower case then checks for each possible outcome of name, start date, shift time, and hourly rate.
                
                if(modify == "name"){
                    Console.WriteLine("What would you like to change the name to?");
                    Console.Write("> ");
                    list[i].Name = Console.ReadLine();
                }
                
                if(modify == "start date" || modify == "date"){
                    DateOnly current = list[i].StartDate;
                    int year = current.Year;
                    int month = current.Month;
                    int day = current.Day;
                    Console.WriteLine("Change: Year | Month | Day");
                    Console.Write("> ");
                    string part = Console.ReadLine().ToLower();
                    if (part == "year")
                    {
                        Console.Write("New year: ");
                        year = int.Parse(Console.ReadLine());
                    }
                    else if (part == "month")
                    {
                        Console.Write("New month: ");
                        month = int.Parse(Console.ReadLine());
                    }
                    else if (part == "day")
                    {
                        Console.Write("New day: ");
                        day = int.Parse(Console.ReadLine());
                    }
                    list[i].StartDate = new DateOnly(year, month, day);
                }
                
                if(modify == "shift time" || modify == "time"){
                    if(list[i].Shift == false){
                        list[i].Shift = true;
                        Console.WriteLine("The shift time has been changed to night.");
                    }
                    else if(list[i].Shift == true){
                        list[i].Shift = false;
                        Console.WriteLine("The shift time has been changed to day.");
                    }
                }
                
                if(modify == "hourly rate" || modify == "rate"){
                    Console.WriteLine("What would you like to change the hourly rate to?");
                    Console.Write("> ");
                    list[i].HourlyRate = double.Parse(Console.ReadLine());
                }
                //checks to see if the user wishes to make more modifications
                Console.WriteLine("What would you like to modify anything else? Y/N");
                Console.Write("> ");
                
                string YesOrNo = Console.ReadLine().ToLower();
                bool Continuing = (YesOrNo == "y" || YesOrNo == "yes");
                if(Continuing){
                    continue;
                }
                else{
                    break;
                }
            }
            i++;
        }
    }

    static void ComputePay(List<Subcontractor> list)
    {
        Console.WriteLine("How many hours has the team worked for?");
        Console.Write("> ");
        if (!int.TryParse(Console.ReadLine(), out int hoursWorked))
        {
            Console.WriteLine("ERROR: Not a valid number.");
            return;
        }

        // Table header
        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine($"{"Subcontractor Name",-25} | {"Gross Pay",10}");
        Console.WriteLine(new string('-', 40));

        if (list.Count == 0) { Console.WriteLine("List is empty."); return; }
        for (int i = 0; i < list.Count; i++)
        {
            var sub = list[i];

            // Calculate pay
            float payMultiplier = sub.Shift ? 1.03f : 1.0f;
            float totalPay = hoursWorked * Convert.ToSingle(sub.HourlyRate) * payMultiplier;

            // Print row
            Console.WriteLine($"{sub.Name,-25} | {totalPay,10:C}");
        }
        // Print footer
        Console.WriteLine(new string('-', 40));
    }
}
