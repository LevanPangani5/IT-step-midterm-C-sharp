﻿using System.Text.RegularExpressions;

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
            char[] options = { '1', '2', '3', '4', '5','6' };
            char option;
           
            Console.WriteLine("## Book managment ##");
            //offers user Book managment funtionality
            do
            {

                Console.WriteLine("Choose coresponding number for desired functinality: ");
                Console.WriteLine("1 - Add a book");
                Console.WriteLine("2 - View all the books");
                Console.WriteLine("3 - Find book by author");
                Console.WriteLine("4 - Find book by title");
                Console.WriteLine("5 - delete book by title");
                Console.WriteLine("6 - stop the program");
                option = Console.ReadKey(true).KeyChar;
               
                Console.Write("\n\n");
                if (!options.Contains(option))
                {
                    Console.WriteLine("Invalid option try again");
                    continue;
                }
                if (option == '6')
                {
                    break;
                }
                RunOperation(option);
                Console.Write("\n");
            } while (true);
        }
        //runs operation that user has chosen
        private void RunOperation(char option)
        {
            switch (option)
            {
                case '1':
                    {
                        Console.WriteLine("Add a book: ");
                        string title = EnterTitle();
                        string author = EnterAuthor();
                        int publishYear = EnterYear();
                        AddBook(title, author, publishYear);
                        break;
                    }
                case '2':
                    {
                        Console.WriteLine("Get all the books: ");
                        GetBooks();
                        break;
                    }
                case '3':
                    {
                        Console.WriteLine("Get book by author: ");
                        string author = EnterAuthor();
                        GetBooks(author);
                        break;
                    }
                case '4':
                    {
                        Console.WriteLine("Get book by title: ");
                        string title = EnterTitle();
                        GetBook(title);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Delete book: ");
                        string title = EnterTitle();
                        DeleteByTitle(title);
                        break;
                    }

            }
        }
        //adds book to book list
        private  Book AddBook(string title, string author, int date)
        {
            Book book = new(title, author, date);
            Books.Add(book);
            Console.WriteLine($"new Book was added: {book}");
            return book;
        }
        //prints all the books
        private void GetBooks()
        {
            Console.WriteLine($"View All Books({Books.Count}): ");
            Books.ForEach(book => Console.WriteLine(book));
        }
        //get books by author
        private void GetBooks(string Author)
        {
            var AuthorBooks = Books.Where(book=> book.Author == Author);
            Console.WriteLine($"{Author}'s Books(${AuthorBooks.Count()}");
            foreach(Book book in AuthorBooks)
            {
                Console.WriteLine(book);
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
            Console.WriteLine(book);
            
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
            do
            {
                Console.Write("Enter valid year of publish: ");
                if (!int.TryParse(Console.ReadLine(), out value))
                {
                    continue;
                }
                Console.Write("\n");
            } while (value < 900 || value > DateTime.Now.Year);
            return value;
        }
    }
}
