using System.Text.RegularExpressions;

namespace IT_step_midterm.Book_Admin
{
    public class BookManager
    {
        private List<Book> Books;

        public BookManager()
        {
            Books = new List<Book>();
        }

        public void RunBookManager()
        {
            char[] options = { '1', '2', '3', '4' };
            char option;
            ConsoleKeyInfo keyInfo = new();
           
            Console.WriteLine("");
            //offers user Book managment funtionality
            do
            {

                Console.WriteLine("Choose coresponding number for desired functinality: ");
                Console.WriteLine("1 - Add a book");
                Console.WriteLine("2 - View all the books");
                Console.WriteLine("3 - Find book by author");
                Console.Write("4 - Find book by title");
                Console.Write("5 - delete book by title");

                option = Console.ReadKey(true).KeyChar;
                Console.Write("\n\n");
                if (!options.Contains(option))
                {
                    Console.WriteLine("Invalid option try again");
                    continue;
                }
                RunOperation(option);

                Console.Write("\n\nIf you want to colse menu click tab\nClick any other key to continue: ");
                keyInfo = Console.ReadKey(true);
                Console.Write("\n");
            } while (keyInfo.Key != ConsoleKey.Tab);
        }
        //runs operation that user has chosen
        private void RunOperation(char option)
        {
            switch (option)
            {
                case '1':
                    {
                        string title = EnterTitle();
                        string author = EnterAuthor();
                        int publishYear = EnterYear();
                        AddBook(title, author, publishYear);
                        break;
                    }
                case '2':
                    {
                        GetBooks();
                        break;
                    }
                case '3':
                    {
                        string author = EnterAuthor();
                        GetBooks(author);
                        break;
                    }
                case '4':
                    {
                        string title = EnterTitle();
                        GetBook(title);
                        break;
                    }
                default:
                    {
                        string title = EnterTitle();
                        DeleteByTitle(title);
                        break;
                    }

            }
        }
        //adds book to book list
        private  Book AddBook(string title, string author, int date)
        {
            Book book = new Book(title, author, date);
            Books.Add(book);
            Console.WriteLine($"new Book was added: {book.ToString}");
            return book;
        }
        //prints all the books
        private void GetBooks()
        {
            Console.WriteLine($"View All Books({Books.Count}): ");
            Books.ForEach(book => book.ToString());
        }
        //get books by author
        private void GetBooks(string Author)
        {
            var AuthorBooks = Books.Where(book=> book.Author == Author);
            Console.WriteLine($"{Author}'s Books(${AuthorBooks.Count()}");
            foreach(Book book in AuthorBooks)
            {
                Console.WriteLine(book.ToString());
            }
        }
        //get book by title
        private void GetBook(string title)
        {
            var book = Books.Find(book => book.Title == title);
            if(book== null)
            {
                Console.WriteLine("Book was not found :(");
            }
            Console.WriteLine("Found Book By this title: ");
            Console.WriteLine(book?.ToString());
            
        }
        //deletes book from list by title
        private void DeleteByTitle(string title)
        {
            Books.RemoveAll(book => book.Title == title);
            Console.WriteLine($"Any book with title: {title} was removed");
        }
        //gets and validates user input for book title
        private string EnterTitle()
        {
            string Id;
            Regex IdRegex = new(@"^[\w\d\s$!?().]{2,30}$");

            do
            {
                Console.Write($"Enter valid book title : ");
                Id = Console.ReadLine() ?? "";
            } while (!IdRegex.IsMatch(Id));
            return Id;
        }
        //gets and validates user input for Author name
        private string EnterAuthor()
        {
            string Id;
            Regex IdRegex = new(@"^[\w\s]{2,30}$");

            do
            {
                Console.Write($"Enter valid Name : ");
                Id = Console.ReadLine() ?? "";
            } while (!IdRegex.IsMatch(Id));
            return Id;
        }
        //gets and validates user input for publish year
        private int EnterYear()
        {  
            int value = -1;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Enter valid year of publish: ");
                if(int.TryParse(Console.ReadLine(), out value)){
                    //value can't be lower than 900 and greater than current year
                    if(value !<900 && value <= DateTime.Now.Year)
                    {
                        return value;
                    }
                }
                Console.Write("\n");
            }
            return value;
        }
    }
}
