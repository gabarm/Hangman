using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            int userInput;


            do
            {
                Console.Clear();
                Console.WriteLine("-----------MENU------------");
                Console.WriteLine("#1 Start Hangman!");
                Console.WriteLine("#0 Exit Game!");

                userInput = MenuSelection();

                switch (userInput)
                {
                    case 1:
                        Game();
                        break;

                    case 0:
                        Console.WriteLine("Press Any key to Quit!");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Wrong input, try again!");
                        break;
                }


            } while (userInput != 0);

        }
        static void Game()
        {
            string[] diffWords = new string[10] { "Programmer", "Belly", "Ending", "Pencil",
                                                  "Dragon", "warlock", "Firewall", "Attend", "Skate", "Truck" };

            Random rnd = new Random();

            string randomWord = diffWords[rnd.Next(0, diffWords.Length)].ToUpper();
            string[] underScore = new string[randomWord.Length];
            char[] correctGuesses = new char [underScore.Length];

            string guess="";
            bool finished = false;
            int maxGuess = 10;
            int count = 0;
            int lettersReveald = 0;
            char input;
            char.TryParse(guess, out input);
            int numbers;
            int.TryParse(guess, out numbers);


            StringBuilder incorrectLetters = new StringBuilder();
            for (int i = 0; i < correctGuesses.Length; i++)
            {
                Array.Fill(correctGuesses, '_');
            }

            Console.Write("Guess a Letter or the complete word! \n\n");
            Console.WriteLine("You have 10 tries to guess right! \n");

            WriteCharArray(correctGuesses);
            
            while (!finished || maxGuess < 11)
            {
                Console.WriteLine("\n");
                guess = Console.ReadLine().ToUpper();
                count++;
                char.TryParse(guess, out input);

                Console.WriteLine("\nYou have used "+ count +" out of 10 guesses!\n");

                if (int.TryParse(guess, out numbers))
                {
                    Console.WriteLine("wrong input");
                    count--;
                }
                if (count==maxGuess)
                {
                    Console.WriteLine("Sorry, Too many guesses. Correct word was: " + randomWord);
                    Console.WriteLine("Press enter to continue!");
                    Console.ReadLine();
                    break;
                }

                for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (randomWord[i] == input)
                        {
                            correctGuesses[i] = randomWord[i];
                            lettersReveald++;
                        }
                    }

                if (maxGuess==11)
                {
                    Console.WriteLine("sorry you lost. Correct Word was: " + randomWord);
                }

                if (lettersReveald == randomWord.Length && maxGuess != 11)
                {
                        WinningMessage(randomWord, maxGuess);
                        Console.ReadLine();
                    break;
                }
                if (guess.Length == randomWord.Length && guess == randomWord)
                {
                    WinningMessage(randomWord, count);
                    Console.ReadLine();
                    finished = true;
                    break;
                }

                else if (guess.Length == randomWord.Length && guess != randomWord)
                {
                    Console.WriteLine("Sorry wrong word! Guess again.\n");
                                        
                }
                else if (guess.Length == 1 && !guess.Contains(randomWord))
                {
                    if (incorrectLetters.ToString().Contains(guess))
                    {
                        incorrectLetters.Replace(guess, "");
                        Console.WriteLine("\nSorry already tried that letter. Guess again.\n ");
                        count--;
                    }
                    Console.Write("\nYou have guessed theese letters: \n");

                    for (int i = 0; i < guess.Length; i++)
                    {
                        incorrectLetters.Append(guess[i]);

                        if (int.TryParse(guess, out numbers))
                        {
                            incorrectLetters.Replace(guess, "");
                        }

                        for (int j = 0; j < incorrectLetters.Length; j++)
                        {

                            Console.Write(incorrectLetters[j] + ", " );

                        }

                    }
                    Console.Write("\n\n");
                }
                else if (guess.Length != randomWord.Length && guess.Length > 1 && guess.Length > randomWord.Length)
                {
                    Console.WriteLine("\nSorry, number of letters you enteres does not match the " +
                        "number of letter of secret word");
                    Console.WriteLine("\nGuess again!");
                    count--;
                    
                }
                else if (guess.Length == 1 && guess.Contains(randomWord))
                {
                    for (int i = 0; i < guess.Length; i++)
                    {
                        Array.Fill(correctGuesses, guess[i]);
                        
                    }
                    Console.Write("\nGuess again \n");
                }

                WriteCharArray(correctGuesses);
            }
        }
        static int MenuSelection()
        {
            int number;
            string inputFromUser = Console.ReadLine();


            while (!int.TryParse(inputFromUser, out number))
            {
                Console.WriteLine("Sorry, Wrong input! Try again.");
                inputFromUser = Console.ReadLine();
                int.TryParse(inputFromUser, out number);
            }
            return number;

        }
        static void WriteCharArray(char[]array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
        }

        static void WinningMessage(string randomWord, int maxguess)
        {
            Console.WriteLine("Congratulations! You have guessed correct!\n" +
                       "the correct word was: " + randomWord + "\n");
            Console.WriteLine("Number of guesses used: " + maxguess);
            Console.Write("\nPress enter to continue to menu.");
        }
    }
}

