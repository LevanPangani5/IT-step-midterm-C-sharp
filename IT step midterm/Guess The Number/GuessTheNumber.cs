namespace IT_step_midterm.Guess_The_Number
{
    public class GuessTheNumber
    {
        //starts the program
        public static void RunGuessNumber()
        {
            int number;
            ConsoleKeyInfo keyInfo;
            Console.WriteLine("## Guess the number ##");
            do
            {   //random namber
                number = GenerateNumber();
                //starts the game
                PlayGuessNumber(number);
                Console.WriteLine("Click y to Play again anything else to Exist ");
                keyInfo = Console.ReadKey();
                Console.Write("\n");
            } while (keyInfo.Key == ConsoleKey.Y);

        }
        //logic for guess the game 
        private static void PlayGuessNumber(int number)
        {
            int guess;
            int attempts = 0;
            ConsoleKeyInfo keyInfo = new();
            
            Console.Write("Guess a number: ");
            do
            {     //validates user input
                if (!int.TryParse(Console.ReadLine(), out guess))
                {
                    Console.WriteLine("Enter a vallid number!!\n");
                    continue;
                }
                attempts++;
                if (number > guess)
                {
                    Console.Write("\nTry Higher: ");
                }
                else if (number < guess)
                {
                    Console.Write("\nTry Lower: ");
                }
                //after every 5 mistakes offers user to stop the game
                if (attempts % 5 == 0)
                {
                    Console.Write("\nIf you want to give up write y if don't enything else: ");
                    keyInfo = Console.ReadKey(true);
                    Console.Write("\n");
                }
                //loop does not stop until user gives up or guess the number
            } while (guess != number && keyInfo.Key != ConsoleKey.Y);
            //displays final statistics
            if (keyInfo.Key == ConsoleKey.Y)
            {
                Console.WriteLine("You've Lost :(");
            }
            else
            {
                Console.WriteLine("You've won :)");
            }
            Console.WriteLine("Atempts: " + attempts);
            Console.WriteLine("Secret number was " + number);
        }

        //functinality for generating ramdom numbers
        private static int GenerateNumber()
        {
            int num1;
            int num2;
            bool isNumber = false;
            while (true)
            {
                Console.WriteLine("Enter a lower limit: ");
                if (!int.TryParse(Console.ReadLine(), out num1))
                {
                    Console.WriteLine("Enter a valid number \n\n");
                    continue;
                }
                do {
                    Console.WriteLine("Enter a upper limit it must be greater than lower limit: ");
                    isNumber = int.TryParse(Console.ReadLine(), out num2);
                    if (!isNumber)
                    {
                        Console.WriteLine("Enter a valid number \n\n");
                        continue;
                    }
                    if(num1 >= num2)
                    {
                        Console.WriteLine("Upper limit mus be greater than lower limit\n\n");
                    }

                }while(num2 <= num1 || !isNumber);

                Random rand = new();
                return rand.Next(num1, num2);
        }
        }
    }
}
