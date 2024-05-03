using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Include JSON.NET library for JSON serialization/deserialization

// Class representing a single journal entry
public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    // Constructor for initializing a new entry with date, prompt, and response
    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }
}

// Main class to manage the journal operations
public class Journal
{
    private List<Entry> entries;

    // Constructor initializes the journal with an empty list of entries
    public Journal()
    {
        entries = new List<Entry>();
    }

    // Function to add a new journal entry by asking user for a response to a random prompt
    public void AddEntry()
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd"); // Get current date in YYYY-MM-DD format
        string prompt = GetRandomPrompt(); // Get a random prompt from predefined list
        Console.WriteLine(prompt);
        string response = Console.ReadLine(); // Capture user response
        entries.Add(new Entry(date, prompt, response)); // Add new entry to list
    }

    // Display all journal entries in a formatted manner
    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}, Prompt: {entry.Prompt}, Response: {entry.Response}");
        }
    }

    // Save the journal entries to a file using JSON serialization for enhanced data integrity and readability
    public void SaveJournal()
    {
        Console.Write("Enter filename to save: ");
        string fileName = Console.ReadLine();
        string json = JsonConvert.SerializeObject(entries, Formatting.Indented); // Serialize list to JSON with indentation
        File.WriteAllText(fileName, json); // Write JSON string to file
        Console.WriteLine("Journal saved.");
    }

    // Load journal entries from a file using JSON deserialization
    public void LoadJournal()
    {
        Console.Write("Enter filename to load: ");
        string fileName = Console.ReadLine();
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            entries = JsonConvert.DeserializeObject<List<Entry>>(json); // Deserialize JSON string back into list of Entry objects
            Console.WriteLine("Journal loaded.");
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }

    // Generate a random journal prompt from a predefined list
    private string GetRandomPrompt()
    {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
        Random rand = new Random();
        return prompts[rand.Next(prompts.Length)];
    }
}

// Main execution point of the program
class Program
{
    static void Main(string[] args)
    {
        Journal myJournal = new Journal();
        bool running = true;
        // Command loop to interact with the user via console
        while (running)
        {
            Console.WriteLine("Choose an option: [1] Add Entry [2] Display Journal [3] Save Journal [4] Load Journal [5] Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    myJournal.AddEntry(); // Add a new entry to the journal
                    break;
                case "2":
                    myJournal.DisplayJournal(); // Display all journal entries
                    break;
                case "3":
                    myJournal.SaveJournal(); // Save journal to file
                    break;
                case "4":
                    myJournal.LoadJournal(); // Load journal from file
                    break;
                case "5":
                    running = false; // Exit the program
                    break;
                default:
                    Console.WriteLine("Invalid choice."); // Handle unexpected input
                    break;
            }
        }
    }
}
