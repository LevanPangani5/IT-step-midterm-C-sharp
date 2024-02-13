
using System.Text.RegularExpressions;

namespace IT_step_midterm.ATM
{
    public class Atm
    {
        private List<string[]> usersData=new();
        readonly string filePath = "C:/me/projects/sweeft/IT step midterm/IT step midterm/ATM/users.txt";
        public Atm()
        {  
            try
            { 
                using StreamReader reader = new(filePath);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    usersData.Add(line.Split(','));
                }
            }catch(Exception e)
            {
               Console.WriteLine($"Error occured during fetching data\n{e.Message}");
            }
        }
        //program starting point
        public void RunAtm()
        {
            char[] options = { '1', '2', '3', '4' };
            char option;
            ConsoleKeyInfo keyInfo =new();
            //saves users Prsonal ID before offering other functinality
            string Id = EnterId();
            Console.WriteLine("");
            //offers user ATM funtionality
            do
            {

                Console.WriteLine("Choose coresponding number for desired functinality: ");
                Console.WriteLine("1 - view balance");
                Console.WriteLine("2 - Withdraw");
                Console.WriteLine("3 - Deposit");
                Console.Write("4 - Transfer");

                option = Console.ReadKey(true).KeyChar;
                Console.Write("\n\n");
                if (!options.Contains(option))
                {
                    Console.WriteLine("Invalid option try again");
                    continue;
                }
                RunOperation(Id, option);
                 
                Console.Write("\n\nIf you want to colse menu click tab\n click any other key to continue: ");
                keyInfo = Console.ReadKey(true);
                Console.Write("\n");
            } while (keyInfo.Key != ConsoleKey.Tab);
        }
        //excecutes specified operations
        public void RunOperation(string Id, char option)
        {
            switch (option)
            {
                case '1':
                    {
                        ViewBalance(Id);
                        break;
                    }
                case '2':
                    {
                        double money = EnterMoney();
                        WithdrawMoney(Id, money);
                        break;
                    }
                case '3':
                    {
                        double money = EnterMoney();
                        DepositMoney(Id, money);
                        break;
                    }
                default:
                    {
                        string receiver = EnterId();
                        double money = EnterMoney();
                        TransferMoney(Id, receiver, money);
                        break;
                    }
            }
        }
        //validates user's Personal ID
         private string EnterId()
        {
            string Id;
            Regex IdRegex = new(@"^\d{11,11}$");

            do{
                Console.Write("Enter valid Personal Id: ");
                Id=Console.ReadLine()??"";
            }while(!IdRegex.IsMatch(Id));
            return Id;
        }
        //validates user input for money
        private double EnterMoney()
        {
            double value=-1;
            bool result=false;
            while (!result || value <=0)
            {
                Console.Write("Enter valid amount of money: ");
                result = double.TryParse(Console.ReadLine(), out value);
                Console.Write("\n");
            }
            return value;
        }
        //returns balance for specifc Personal ID
        private double ViewBalance(string Id)
         {
            int userIndex = FindUser(Id);
            {
                if (userIndex!=-1)
                {
                    string balance = usersData[userIndex][3][8..];
                    Console.WriteLine($"{Id} Balance: {balance}");
                    return double.Parse(balance);
                }
            }
            return -1;
         }
        //Withdraws specified amount of moeny from specified user
        private bool WithdrawMoney(string Id, double money)
        {
            double balance= ViewBalance(Id);
            if(balance == -1)
            {
                return false;
            }
            //stops if withdraw moeny is greater than balance
            if (money > balance)
            {
                Console.WriteLine("Withdraw opperation was canceled due to insufficient funds");
                Console.WriteLine($"{Id} balance: {balance}");
                return false;
            }
            //makes changes in the file
            balance -= money;
            if (Save(Id, balance))
            {
                Console.WriteLine($"Withdrawed: {money}$");
                Console.WriteLine($"{Id} balance: {balance}");
            }
            else
            {
                Console.WriteLine("Error occured ");
            }

            return true;
        }
        //depposits moeny in the users account
        private bool DepositMoney(string Id, double money)
        {
            double balance = ViewBalance(Id);
            if (balance == -1)
            {
                return false;
            }
            //makes changes in the file
            balance+= money;
            if(Save(Id, balance))
            {
              Console.WriteLine($"Deposited: {money}$");
              Console.WriteLine($"{Id}  balance: {balance}");
                return true;
            }
            else
            {
                Console.WriteLine("Error occured ");
                return false;
            }
           
        }
        //transfers money from one user to another
        private bool TransferMoney(string sender,string receiver, double money)
        {
            int senderIndex = FindUser(sender); 
            int receiverIndex= FindUser(receiver);
            //if sender or receiver users does not exist stops operation
            if(senderIndex == -1|| receiverIndex == -1)
            {
                Console.WriteLine("Operation was canceled bue to invalid First ID");
                return false;
            } 
           //withdraws money from sender and deposits it receiver
           if(WithdrawMoney(sender, money))
            {
                return DepositMoney(receiver, money);
            }
            return false;
        }
        //functinality for saving changes in the file
        private bool Save(string Id,double balance)
        {
            int userIndex = FindUser(Id);
            if (userIndex == -1)
            {
                return false;
            }
            //updates data locally
            string newBalance = $"Balance:{balance}";
            usersData[userIndex][3]=newBalance;
            //updates data in the file
            using(StreamWriter writer = new(filePath, false))
            {
                try
                {     
                    foreach(var user in usersData)
                    {
                        writer.WriteLine(string.Join(",",user));
                    }
                }catch(Exception e)
                {
                    Console.WriteLine($"Error occured during saving changes\n{e.Message}");
                }
            }
            Console.WriteLine("Data was updated succesffuly");
            return true;

        }
        //finds user in the list and returns index
        private int FindUser(string Id)
        {
            for(int i =0; i < usersData.Count; i++)
            {
                if (usersData[i][0][3..] == Id)
                {
                    return i;
                }
            }
            Console.WriteLine($"User with ID:{Id} was not found");
            return -1;
        }
    }
}
