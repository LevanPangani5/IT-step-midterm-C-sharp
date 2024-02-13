using IT_step_midterm.ATM;
using IT_step_midterm.Guess_The_Number;
using IT_step_midterm.Book_Admin;
using IT_step_midterm.Student_Admin;

namespace IT_step_midterm.Menu
{
    public class Menu
    {
        private static Atm atm = new();
        private static BookManager bookMAnager=new();
        private static StudentManager studentManager = new();
        //runs manue program , it lets user to interact with all the other proograms
        public static void RunMenu()
        {   //options for different programs
            char[] options = { '1', '2', '3', '4','5','6' };
            char option;
            ConsoleKeyInfo keyInfo = new();
            do
            {
                Console.WriteLine("Choose coresponding number for desired functinality: ");
                Console.WriteLine("1 - ATM");
                Console.WriteLine("2 - Hangman");
                Console.WriteLine("3 - Guess the number");
                Console.WriteLine("4 - Calculator");
                Console.WriteLine("5 - Book Managment");
                Console.WriteLine("6 - Student Managment");

                option = Console.ReadKey(true).KeyChar;
                Console.Write("\n\n");
                if (!options.Contains(option))
                {
                    Console.WriteLine("Invalid option try again");
                    continue;
                }
                RunOperation(option);

                Console.Write("If you want to colse menu click tab\n click any other key to continue: ");
                keyInfo = Console.ReadKey(true);
                Console.Write("\n");
            } while (keyInfo.Key != ConsoleKey.Tab);
        }
        //logic to run program chosen by the user
        private static void RunOperation(char option)
        {
            switch (option)
            {
                case '1':
                    {
                        atm.RunAtm();
                        break;
                    }
                case '2':
                    {
                        Hangman.Hangman.StartHangman();

                        break;
                    }
                case '3':
                    {
                        GuessTheNumber.RunGuessNumber();
                        break;
                    }
                case '4':
                    {
                        Calculator.Calculator.RunCalculator();
                        break;
                    }
                case '5':
                    {
                        bookMAnager.RunBookManager();
                        break;
                    }
                case '6':
                    {
                        studentManager.RunStudentManager();
                        break;
                    }
            }
        }
    }
}
