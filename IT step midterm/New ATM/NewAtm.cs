using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace IT_step_midterm.New_ATM
{
    public class NewAtm
    {   //logged in user's data
        private static JObject userData;
        //name of the file where users datas are saved
        List<string> fileNames;
        //folder path which holds users information
        private readonly string folderPath = "C:/me/projects/sweeft/IT step midterm/IT step midterm/New ATM/Data";
        private string filePath;
        public NewAtm()
        {
            fileNames = new();
            try
            {    //loop over directoy to get names of the files
                foreach (var file in Directory.GetFiles(folderPath))
                {
                    Console.WriteLine(Path.GetFileName(file));
                    fileNames.Add(Path.GetFileName(file));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured during fetching data\n{e.Message}");
            }
        }
        //program starting point
        public void RunAtm()
        {
            char[] options = { '1', '2', '3', '4', '5', '6', '7' };
            char option;
            //offres users to login or sign up and than login
            if (RunIntro())
            {
                Console.WriteLine($"Hello {userData["firstName"]}");
            }
            else
            {
                return;
            }

            Console.WriteLine("## ATM ##");
            //offers user ATM funtionality
            do
            {
                Console.WriteLine("Choose coresponding number for desired functinality: ");
                Console.WriteLine("1 - view balance");
                Console.WriteLine("2 - Withdraw");
                Console.WriteLine("3 - Deposit");
                Console.WriteLine("4 - view transactions");
                Console.WriteLine("5 - change pin");
                Console.WriteLine("6 - transfer money");
                Console.WriteLine("7 - stop the program");
                option = Console.ReadKey(true).KeyChar;
                Console.Write("\n");
                if (!options.Contains(option))
                {
                    Console.WriteLine("Invalid option try again");
                    continue;
                }
                if (option == '7')
                {
                    break;
                }
                RunOperation(option);
                Console.Write("\n");
            } while (true);
        }
        //excecutes specified operations
        public void RunOperation(char option)
        {
            switch (option)
            {
                case '1':
                    {
                        Console.WriteLine("View balance: ");
                        ViewBalance();
                        break;
                    }
                case '2':
                    {
                        Console.WriteLine("Withdraw money: ");
                        WithdrawMoney(EnterMoney());
                        break;
                    }
                case '3':
                    {
                        Console.WriteLine("Deposit money: ");
                        DepositMoney(EnterMoney());
                        break;
                    }
                case '4':
                    {
                        Console.WriteLine("View past 5 operaations: ");
                        ViewTransactions();
                        break;
                    }
                case '5':
                    {
                        Console.WriteLine("Change Pin: ");
                        ChangePin();
                        break;
                    }
                case '6':
                    {

                        Console.WriteLine("Transfer money");
                        Console.WriteLine("Enter Reciver's card number: ");
                        string receiver = EnterNum("Card number", 16);
                        double money = EnterMoney();
                        Transfer(money, receiver);

                        break;
                    }
            }
        }
        //offres user sign in por sign up and sign in
        public bool RunIntro()
        {
            char option;
            do
            {
                Console.WriteLine("1 - sign up");
                Console.WriteLine("2 - sign in");
                option = Console.ReadKey().KeyChar;
                Console.WriteLine("");
                switch (option)
                {
                    case '1':
                        {
                            if (SignUp())
                            {
                                return Login();
                            }
                            break;
                        }
                    case '2':
                        {
                            return Login();
                        }
                }
            } while (true);
        }
        //sign up logic
        public bool SignUp()
        {
            string cardNumber;
            string cvc;
            string expiraitionDate;
            string? fileName;
            JObject? data = null;
            //getting all the nesseracry info from the user
            do
            {
                cardNumber = EnterNum("Card number", 16);
                fileName = FindFile(cardNumber);
                if (fileName != null)
                {
                    Console.WriteLine("This card number is aleady registered credentials ");
                    break;
                }
                cvc = EnterNum("CVC", 3);
                expiraitionDate = EnterExpirationDate();
                //check if card number is already used
                if (data == null)
                {   //get the schema to creat file for new user
                    data = GetUserData("C:/me/projects/sweeft/IT step midterm/IT step midterm/New ATM/Shema.json");
                    Console.WriteLine(data);
                    if (data == null)
                        break;
                    //filling JObject with user informationn
                    Random rand = new();
                    char[] pin = new char[4];
                    data["firstName"] = "firstName";
                    data["lastName"] = "lastName";
                    JObject cardDetails = (JObject)data["cardDetails"];
                    cardDetails["cardNumber"] = cardNumber;
                    cardDetails["expirationDate"] = expiraitionDate.Insert(2, "/"); ;
                    cardDetails["CVC"] = cvc;

                    //pin generation
                    for (int i = 0; i < 4; i++)
                    {
                        pin[i] = (char)(rand.Next(0, 10) + '0');
                    }
                    string pinStr = new(pin);
                    data["pinCode"] = pinStr;
                    Console.WriteLine("!!! Remember Your pin: " + pinStr + " !!!");
                    //adding first transaction in transactionHistory
                    JObject newTransaction = new(
                    new JProperty("transactionDate", DateTime.Now),
                    new JProperty("transactionType", "register"),
                    new JProperty("amountGEL", 0)
                    );
                    JArray? transactions = (JArray)data["transactionHistory"]!;
                    transactions.Add(newTransaction);

                    string newFilePath = folderPath + $"{cardNumber}_{cvc}_{expiraitionDate}.json";

                    try
                    { //creating file and writing data in the file
                        File.WriteAllText(newFilePath, data.ToString());
                        fileNames.Add($"{cardNumber}_{cvc}_{expiraitionDate}.json");
                        Console.WriteLine("New Account was created!");
                        Console.WriteLine("You can log in now!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error occured while creating user file" + e.Message);
                    }
                }
            } while (true);

            return false;
        }
        //login logic
        public bool Login()
        {
            string cardNumber;
            string cvc;
            string pin;
            string expiraitionDate;
            string? fileName = "";
            JObject? data = null;
            do
            {  //getting all the nessesary inf from the user
                cardNumber = EnterNum("Card number", 16);
                cvc = EnterNum("CVC", 3);
                expiraitionDate = EnterExpirationDate();
                fileName = FindFile(cardNumber, cvc, expiraitionDate);
                //check if user with that info is registered
                if (fileName == null)
                {
                    Console.WriteLine("Invalid credentials ");
                    break;
                }

                if (data == null)
                {
                    data = GetUserData(folderPath + fileName);
                    if (data == null)
                        break;
                }
                //check if pin is valid
                pin = EnterNum("PIN", 4);
                if (pin == data["pinCode"]?.ToString())
                {
                    userData = data;
                    filePath = folderPath + fileName;
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid PIN try again");
                }

            } while (true);

            return false;
        }
        //logic for get and validate expiration date
        private string EnterExpirationDate()
        {
            string month;
            string year;
            // Regex IdRegex = new(@"^([0-9]{2,2})\/([0-9]{2,2})$");
            month = EnterNum("month number", 2);
            year = EnterNum("year (last two numbers)", 2);
            month += year;
            return month;
        }
        //logic to get and validate any kind of number related string (cardNumber,pin,cvc,...)
        private string EnterNum(string label, int length)
        {
            string value;
            string pattern = $"^[0-9]{{{length},{length}}}$";
            Regex IdRegex = new(pattern);
            do
            {
                Console.Write($"Enter valid {label}: ");
                value = Console.ReadLine() ?? "";
            } while (!IdRegex.IsMatch(value));
            return value;
        }
        //logic to get aand validate input for money
        private double EnterMoney()
        {
            double value = -1;
            bool result = false;
            while (!result || value <= 0)
            {
                Console.Write("Enter valid amount of money: ");
                result = double.TryParse(Console.ReadLine(), out value);
                Console.Write("\n");
            }
            return value;
        }

        //check if file exist with user porovided info
        private string? FindFile(string cardNumber, string cvc, string expirationDate)
        {
            string fileName = $"{cardNumber}_{cvc}_{expirationDate}.json";
            return FindFile(fileName);
        }
        private string? FindFile(string credential)
        {
            string? isFile = fileNames.Find(item => item.Contains(credential));
            return isFile;
        }
        //get Json object from the file
        private JObject? GetUserData(string path)
        {
            try
            {
                return JObject.Parse(File.ReadAllText(path));

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured during fetching data\n{e.Message}");
                return null;
            }
        }
        //view user balance, or reciver's
        private double ViewBalance(JObject? data = null)
        {
            data ??= userData;
            JArray? transactions = (JArray)data["transactionHistory"]!;

            var balance = transactions[0]["amountGEL"];
            Console.WriteLine("Your balace is: " + balance);
            return double.Parse(balance!.ToString());
        }
        //logic for withdrawing money
        private bool WithdrawMoney(double money)
        {
            double balance = ViewBalance();
            //stops if withdraw moeny is greater than balance
            if (money > balance)
            {
                Console.WriteLine("Withdraw opperation was canceled due to insufficient funds");
                return false;
            }
            balance -= money;
             if(RunTransaction("Withdraw", balance))
            {
                Console.WriteLine($"Withdrawed: {money}");
                return true;
            }
            else
            {
                return false;
            }
        }
        //logic for depositting money
        private bool DepositMoney(double money)
        {
            double balance = ViewBalance();
            balance += money;
            if (RunTransaction("Withdraw", balance))
            {
                Console.WriteLine($"Deposit: {money}");
                return true;
            }
            else
            {
                return false;
            }
        }
        //logic for viewing past 5 transactions
        private void ViewTransactions()
        {
            JArray? transactions = (JArray)userData["transactionHistory"]!;
            Console.WriteLine($"Transactions({transactions.Count}): ");
            foreach (var item in transactions)
            {
                Console.WriteLine($"{item["transactionDate"]}");
                Console.WriteLine($"{item["transactionType"]}");
                Console.WriteLine($"{item["amountGEL"]}");
                Console.WriteLine($"---------------------------------------------------\n");
            }
        }
        //logic for changing pin
        private void ChangePin()
        {
            int tries = 0;
            string pin;
            while (true)
            {
                Console.WriteLine("Enter current pin: ");
                pin = EnterNum("PIN", 4);
                if (pin == userData["pinCode"].ToString())
                {
                    break;
                }
                else
                {
                    tries++;
                    if (tries >= 4)
                    {
                        Console.WriteLine("Three wrong attempts ");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"{tries}rd Wrong input try again: \n");
                    }
                }
            }
            Console.WriteLine("choose a new pin");
            string newPin = EnterNum("PIN", 4);
            userData["pinCode"] = newPin;
            RunTransaction("changePin", ViewBalance());
            Save();
            Console.WriteLine("Pin was updated");
        }
        //loggic for transfering money to other user
        private void Transfer(double money, string reciever)
        {
            string? fileName = FindFile(reciever);
            if (fileName == null)
            {
                Console.WriteLine("user With provider card number does not exist");
            }
            string reciverPath = folderPath + fileName;
            double balance = ViewBalance();
            if (balance < money)
            {
                Console.WriteLine("trasfer was canceled due to insufficent funds");
            }
            JObject? data = GetUserData(reciverPath);
            if (data == null)
            {
                return;
            }
            if (!WithdrawMoney(money))
            {
                return;
            }
            double recieverBalance = ViewBalance(data);
            recieverBalance += money;
            JArray? transactions = (JArray)data!["transactionHistory"]!;

            JObject newTransaction = CreateTransactionObj("Deposit", recieverBalance);
            if (transactions.Count == 5)
            {
                transactions.Last.Remove();
            }
            transactions.Insert(0, newTransaction);

            Save(reciverPath, data);
        }
        //adding transaction to transactionHistory and saving it
        private bool RunTransaction(string transactionType, double balance, JObject? data = null)
        {
            data ??= userData;
            JObject newTransaction = CreateTransactionObj(transactionType, balance);
            JArray? transactions = (JArray)data["transactionHistory"]!;

            if (transactions.Count == 5)
            {
                transactions.Last.Remove();
            }
            transactions.Insert(0, newTransaction);

            if (Save())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //creats transacction object which will be added in the transaction history
        JObject CreateTransactionObj(string transactionType, double balance)
        {
            JObject newTransaction = new(
            new JProperty("transactionDate", DateTime.Now),
            new JProperty("transactionType", transactionType),
            new JProperty("amountGEL", balance)
            );

            return newTransaction;
        }

        //functinality for saving changes in the file
        private bool Save(string? fileFullPath = null, JObject? data = null)
        {
            fileFullPath ??= filePath;
            data ??= userData;
            try
            {
                File.WriteAllText(fileFullPath, data.ToString());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ocuured during saving data ...\n Error= " + e.Message);
                return false;
            }
        }
    }
}
