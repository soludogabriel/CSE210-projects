using System;

namespace GuessMyNumberGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int magicNumber = random.Next(1, 101); // Generates a number from 1 to 100
            int maxGuesses = 5; // Maximum number of guesses allowed
            int userGuess = 0; // Initialize outside the loop
            int guessCount = 0; // Initialize guess count

            while (userGuess != magicNumber && guessCount < maxGuesses)
            {
                Console.Write("What is your guess? ");
                userGuess = int.Parse(Console.ReadLine());
                guessCount++;

                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
            }

            if (userGuess == magicNumber)
            {
                Console.WriteLine("You guessed it!");
            }
            else
            {
                Console.WriteLine($"Sorry, you've reached the maximum number of guesses ({maxGuesses}). The magic number was: {magicNumber}");
            }
        }
    }
}
