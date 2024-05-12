using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Class to represent a single word in the scripture
    public class Word
    {
        public string Text { get; set; }
        public bool IsHidden { get; set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }

        public override string ToString()
        {
            return IsHidden ? "_____" : Text;
        }
    }

    // Class to represent a scripture reference
    public class ScriptureReference
    {
        public string Book { get; }
        public int StartVerse { get; }
        public int EndVerse { get; }

        public ScriptureReference(string book, int startVerse, int endVerse)
        {
            Book = book;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public override string ToString()
        {
            if (StartVerse == EndVerse)
            {
                return $"{Book} {StartVerse}";
            }
            else
            {
                return $"{Book} {StartVerse}-{EndVerse}";
            }
        }
    }

    // Class to represent a scripture passage
    public class Scripture
    {
        private readonly List<Word> _words;
        private readonly ScriptureReference _reference;

        public Scripture(ScriptureReference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        public bool AllWordsHidden()
        {
            return _words.All(word => word.IsHidden);
        }

        public void HideRandomWords(int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(_words.Count);
                if (!_words[index].IsHidden)
                {
                    _words[index].Hide();
                }
            }
        }

        public override string ToString()
        {
            return $"{_reference}\n{_words.Aggregate("", (current, word) => current + (word + " "))}";
        }
    }

    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample scripture
            ScriptureReference reference = new ScriptureReference("John", 3, 16);
            string text = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.";
            Scripture scripture = new Scripture(reference, text);

            // Display the complete scripture
            Console.WriteLine(scripture);
            Console.WriteLine("Press Enter to continue or type 'quit' to exit...");

            // Hide words until all are hidden
            while (!scripture.AllWordsHidden())
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    break;
                }
                else
                {
                    // Hide a few random words
                    scripture.HideRandomWords(2); // Hide 2 words at a time for demonstration
                    Console.Clear(); // Clear the console
                    Console.WriteLine(scripture);
                    Console.WriteLine("Press Enter to continue or type 'quit' to exit...");
                }
            }

            Console.WriteLine("Program ended. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
