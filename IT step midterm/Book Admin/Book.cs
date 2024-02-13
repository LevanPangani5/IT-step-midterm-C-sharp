using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_step_midterm.Book_Admin
{
    public class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int PublishYear { get; private set; }

        public Book(string title, string author, int publishYear)
        {
            Title = title;
            Author = author;
            PublishYear = publishYear;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, Publish Year: {PublishYear}";
        }
    }

    
}
