using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    abstract class Goal
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Goal(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public abstract int RecordEvent();
        public abstract string GetDetailsString();
    }

    class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, int value) : base(name, value)
        {
            _isComplete = false;
        }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return Value;
            }
            return 0;
        }

        public override string GetDetailsString()
        {
            return $"{(_isComplete ? "[X]" : "[ ]")} {Name} - {Value} points";
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, int value) : base(name, value) { }

        public override int RecordEvent()
        {
            return Value;
        }

        public override string GetDetailsString()
        {
            return $"[∞] {Name} - {Value} points each time";
        }
    }

    class ChecklistGoal : Goal
    {
        public int TargetCount { get; set; }
        public int CurrentCount { get; set; }
        public int Bonus { get; set; }

        public ChecklistGoal(string name, int value, int targetCount, int bonus) : base(name, value)
        {
            TargetCount = targetCount;
            CurrentCount = 0;
            Bonus = bonus;
        }

        public override int RecordEvent()
        {
            CurrentCount++;
            if (CurrentCount >= TargetCount)
            {
                return Value + Bonus;
            }
            return Value;
        }

        public override string GetDetailsString()
        {
            return $"[{CurrentCount}/{TargetCount}] {Name} - {Value} points each time, {Bonus} bonus when complete";
        }
    }

    class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public void CreateGoal()
        {
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choose a goal type: ");
            int choice = int.Parse(Console.ReadLine());

            Console.Write("Enter the name of the goal: ");
            string name = Console.ReadLine();
            Console.Write("Enter the point value of the goal: ");
            int value = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    _goals.Add(new SimpleGoal(name, value));
                    break;
                case 2:
                    _goals.Add(new EternalGoal(name, value));
                    break;
                case 3:
                    Console.Write("Enter the target count for the goal: ");
                    int targetCount = int.Parse(Console.ReadLine());
                    Console.Write("Enter the bonus points for completing the goal: ");
                    int bonus = int.Parse(Console.ReadLine());
                    _goals.Add(new ChecklistGoal(name, value, targetCount, bonus));
                    break;
            }
        }

        public void RecordEvent()
        {
            ShowGoals();
            Console.Write("Enter the number of the goal you completed: ");
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index >= 0 && index < _goals.Count)
            {
                _score += _goals[index].RecordEvent();
                Console.WriteLine("Event recorded.");
            }
            else
            {
                Console.WriteLine("Invalid goal number.");
            }
        }

        public void ShowGoals()
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        public void DisplayScore()
        {
            Console.WriteLine($"Current score: {_score} points");
        }

        public void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                writer.WriteLine(_score);
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetType().Name);
                    writer.WriteLine(goal.Name);
                    writer.WriteLine(goal.Value);
                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine(checklistGoal.TargetCount);
                        writer.WriteLine(checklistGoal.CurrentCount);
                        writer.WriteLine(checklistGoal.Bonus);
                    }
                }
            }
            Console.WriteLine("Goals saved.");
        }

        public void LoadGoals()
        {
            if (File.Exists("goals.txt"))
            {
                using (StreamReader reader = new StreamReader("goals.txt"))
                {
                    _score = int.Parse(reader.ReadLine());
                    _goals.Clear();
                    while (!reader.EndOfStream)
                    {
                        string type = reader.ReadLine();
                        string name = reader.ReadLine();
                        int value = int.Parse(reader.ReadLine());
                        if (type == "SimpleGoal")
                        {
                            _goals.Add(new SimpleGoal(name, value));
                        }
                        else if (type == "EternalGoal")
                        {
                            _goals.Add(new EternalGoal(name, value));
                        }
                        else if (type == "ChecklistGoal")
                        {
                            int targetCount = int.Parse(reader.ReadLine());
                            int currentCount = int.Parse(reader.ReadLine());
                            int bonus = int.Parse(reader.ReadLine());
                            ChecklistGoal checklistGoal = new ChecklistGoal(name, value, targetCount, bonus)
                            {
                                CurrentCount = currentCount
                            };
                            _goals.Add(checklistGoal);
                        }
                    }
                }
                Console.WriteLine("Goals loaded.");
            }
            else
            {
                Console.WriteLine("No saved goals found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoalManager goalManager = new GoalManager();
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Eternal Quest");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. Show Goals");
                Console.WriteLine("4. Display Score");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        goalManager.CreateGoal();
                        break;
                    case 2:
                        goalManager.RecordEvent();
                        break;
                    case 3:
                        goalManager.ShowGoals();
                        break;
                    case 4:
                        goalManager.DisplayScore();
                        break;
                    case 5:
                        goalManager.SaveGoals();
                        break;
                    case 6:
                        goalManager.LoadGoals();
                        break;
                }
            } while (choice != 7);
        }
    }
}