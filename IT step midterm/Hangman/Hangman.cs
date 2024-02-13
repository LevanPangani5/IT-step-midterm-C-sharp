
namespace IT_step_midterm.Hangman
{
    public class Hangman
    {  
        //starts the game
        public static void StartHangman()
        {
            string word;
            ConsoleKeyInfo keyInfo;
            do
            {
                word = GetRandomWord();
                PlayHangman(word);
                Console.WriteLine("Click y to Play again anything else to Exist ");
                keyInfo = Console.ReadKey();
                Console.Write("\n");
            } while (keyInfo.Key == ConsoleKey.Y);
        }
        //returns random word from the file
        private static string GetRandomWord()
        {
            try
            {
                string[] Lines = File.ReadAllLines("C:/me/projects/sweeft/IT step midterm/IT step midterm/Hangman/words.txt");
                Random rand = new();     
                string[] words = Lines[rand.Next(0, Lines.Length)].Split(',');
                string word = words[rand.Next(0, words.Length)];
                return word;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
        }
        //logic for Hangman game
        private static void PlayHangman(string word)
        {   //correct symbols
            string indexesStr = "";
            //incorrect symbols
            string wrongLetters = "";
            //counts correct guesses
            int indexesCount = 0;
            //counts mistakes
            int mistakes = 0;
            //displays secret word as *
            string secret = new(word.Select(c => '*').ToArray());
            ConsoleKeyInfo keyInfo;
            //loop continues if number of mistakes is lower than 7 or user didn't guess the word
            while (mistakes < 7 && word != secret)
            {

                Console.Write("Enter a letter: ");
                keyInfo = Console.ReadKey();
                Console.WriteLine();
                //checks if user already discovered this symbol
                if (indexesStr.Contains(keyInfo.KeyChar))
                {
                    Console.WriteLine($"{keyInfo.KeyChar} is already used try something else\n\n");
                    continue;
                }
                //checks if symbol is in the word
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == keyInfo.KeyChar)
                    {     
                        indexesStr += keyInfo.KeyChar;
                        secret = secret.Remove(i, 1).Insert(i, keyInfo.KeyChar.ToString());
                    }
                }
                //if they are equal indexesStr was not incremented => user didn't entered the right letter
                if (indexesCount == indexesStr.Length)
                {
                    if (!wrongLetters.Contains(keyInfo.KeyChar))
                        wrongLetters += keyInfo.KeyChar.ToString();
                    mistakes++;
                }
                else
                {
                    indexesCount++;
                }
                //display game statistics
                foreach (char item in secret)
                {
                    Console.Write($" {item}");
                }
                Console.Write($"  ---   Chances left: {7 - mistakes}");
                Console.Write($"  ---   Wrong letters:");
                foreach (char item in wrongLetters)
                {
                    Console.Write($" {item}");
                }
                Console.Write("\n  ");
                DrawHangman(mistakes);
                Console.WriteLine("\n");
            }

            Console.Write("\n\n");
            if (secret == word)
            {
                Console.WriteLine("You Won !!");
            }
            else
            {
                Console.WriteLine("You've Lost :(\nsecret word was: " + word);
            }

        }
        //functinality for drawing hangman 
        private static void DrawHangman(int mistake)
        {
            if (mistake > 7 || mistake < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(mistake), "Value must be less than or equal to 4.");
            }
            Console.Write("___");

            //Handling head
            if (mistake == 7)
            {
                Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop + 1);
                Console.Write("X |");
            }
            else if (mistake > 0)
            {
                Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop + 1);
                Console.Write("O |");
            }
            else
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop + 1);
                Console.Write("|");
            }

            //Handling upper body
            #region upperBody
            if (mistake > 3)
            {
                Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop + 1);
                Console.Write("/|\\|");
            }
            else if (mistake > 2)
            {
                Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop + 1);
                Console.Write("|\\|");
            }
            else if (mistake > 1)
            {
                Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop + 1);
                Console.Write("\\|");
            }
            else
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop + 1);
                Console.Write("|");
            }
            #endregion

            //Handling lower body
            #region lowerBody
            if (mistake > 5)
            {
                Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop + 1);
                Console.Write("/\\ |");
            }
            else if (mistake > 4)
            {
                Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop + 1);
                Console.Write("\\ |");
            }
            else
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop + 1);
                Console.Write("|");
            }
            #endregion
        }
    }
}
