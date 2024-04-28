using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for grade percentage
        Console.Write("Enter your grade percentage: ");
        double gradePercentage = double.Parse(Console.ReadLine());

        // Initialize variables for letter grade and sign
        char letter;
        char sign = ' ';

        // Determine the letter grade
        if (gradePercentage >= 90)
        {
            letter = 'A';
        }
        else if (gradePercentage >= 80)
        {
            letter = 'B';
        }
        else if (gradePercentage >= 70)
        {
            letter = 'C';
        }
        else if (gradePercentage >= 60)
        {
            letter = 'D';
        }
        else
        {
            letter = 'F';
        }

        // Determine the sign for the letter grade
        if (letter != 'F')
        {
            int lastDigit = (int)(gradePercentage % 10);
            if (lastDigit >= 7)
            {
                sign = '+';
            }
            else if (lastDigit < 3)
            {
                sign = '-';
            }
        }

        // Display the letter grade and sign
        Console.Write("Your grade is: ");
        Console.Write($"{letter}");
        if (sign != ' ')
        {
            Console.Write($"{sign}");
        }
        Console.WriteLine();

        // Check if the user passed the course
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Keep working hard. You can do better next time.");
        }
    }
}