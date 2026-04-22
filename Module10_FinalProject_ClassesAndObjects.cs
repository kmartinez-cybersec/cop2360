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
        Console.WriteLine("Case 4 ModifySubcontractor()");
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
