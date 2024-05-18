using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var activities = new List<BaseActivity>
        {
            new BreathingActivity(),
            new ReflectionActivity(),
            new ListingActivity()
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Mindfulness Session. Please select an activity:");
            for (int i = 0; i < activities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {activities[i].Name}");
            }
            Console.WriteLine("Press 0 to exit.");

            int choice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice == -1) break;

            if (choice >= 0 && choice < activities.Count)
            {
                activities[choice].PrepareActivity();
                activities[choice].StartActivity();
                activities[choice].EndActivity();
            }
        }
    }
}

abstract class BaseActivity
{
    public abstract string Name { get; }
    protected int Duration;

    public void PrepareActivity()
    {
        Console.Clear();
        Console.WriteLine($"Activity: {Name}");
        Console.WriteLine("Enter duration of the activity in seconds:");
        Duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        DisplayCountdown(3);
    }

    public abstract void StartActivity();

    public void EndActivity()
    {
        Console.WriteLine("\nYou did a great job!");
        DisplayCountdown(3);
        Console.WriteLine($"You have completed {Name} for {Duration} seconds.");
        DisplayCountdown(3);
    }

    protected void DisplayCountdown(int seconds)
    {
        while (seconds > 0)
        {
            Console.Write($"{seconds}... ");
            Thread.Sleep(1000);
            seconds--;
        }
        Console.WriteLine();
    }
}

class BreathingActivity : BaseActivity
{
    public override string Name => "Breathing Relaxation";

    public override void StartActivity()
    {
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
        int elapsedTime = 0;
        while (elapsedTime < Duration)
        {
            Console.WriteLine("Breathe in...");
            DisplayCountdown(4);
            elapsedTime += 4;

            if (elapsedTime >= Duration) break;

            Console.WriteLine("Breathe out...");
            DisplayCountdown(4);
            elapsedTime += 4;
        }
    }
}

class ReflectionActivity : BaseActivity
{
    public override string Name => "Reflection on Strength";

    public override void StartActivity()
    {
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
        var prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need."
        };
        var random = new Random();
        string selectedPrompt = prompts[random.Next(prompts.Count)];

        Console.WriteLine(selectedPrompt);
        DisplayCountdown(5);  // Give some time for initial reflection.

        var questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "How did you feel when it was complete?",
            "What did you learn from this experience?"
        };

        int elapsedTime = 5;
        while (elapsedTime < Duration)
        {
            string question = questions[random.Next(questions.Count)];
            Console.WriteLine(question);
            DisplayCountdown(5);
            elapsedTime += 5;
        }
    }
}

class ListingActivity : BaseActivity
{
    public override string Name => "Positive Listing";

    public override void StartActivity()
    {
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        var prompts = new List<string>
        {
            "List the people that you appreciate:",
            "List your personal strengths:",
            "List the people you have helped this week:"
        };
        var random = new Random();
        string selectedPrompt = prompts[random.Next(prompts.Count)];

        Console.WriteLine(selectedPrompt);
        DisplayCountdown(5);

        int elapsedTime = 5;
        int count = 0;
        while (elapsedTime < Duration)
        {
            Console.Write("Enter an item: ");
            string item = Console.ReadLine();
            count++;
            elapsedTime += 5;  // Simulating time to think and type.
        }

        Console.WriteLine($"Total items listed: {count}");
    }
}
