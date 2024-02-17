using IT_step_midterm.ATM;
using IT_step_midterm.Guess_The_Number;
using IT_step_midterm.Book_Admin;
using IT_step_midterm.Student_Admin;
using IT_step_midterm.New_ATM;

namespace IT_step_midterm.Menu
{
    public class Menu
    {
        private static Atm atm = new();
        private static BookManager bookMAnager=new();
        private static StudentManager studentManager = new();
        private static NewAtm newAtm = new();
        //runs manue program , it lets user to interact with all the other proograms
        public static void RunMenu()
        {   //options for different programs
            char[] options = { '1', '2', '3', '4','5','6','7','8' };
            char option;
            do
            {
                Console.WriteLine("Choose coresponding number for desired functinality: ");
                Console.WriteLine("1 - ATM");
                Console.WriteLine("2 - Hangman");
                Console.WriteLine("3 - Guess the number");
                Console.WriteLine("4 - Calculator");
                Console.WriteLine("5 - Book Managment");
                Console.WriteLine("6 - Student Managment");
                Console.WriteLine("7 - New ATM");
                Console.WriteLine("8 - Stop the program");
                option = Console.ReadKey(true).KeyChar;
                Console.Write("\n");
                if (!options.Contains(option))
                {
                    Console.WriteLine("Invalid option try again");
                    continue;
                }
                if(option == '8')
                {
                    break;
                }
                RunOperation(option);           
                Console.Write("\n");
            } while (true);
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
                        Hangman.Hangman.RunHangman();

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
                case '7':
                    {
                        newAtm.RunAtm();
                        break;
                    }
            }
        }
    }
}
