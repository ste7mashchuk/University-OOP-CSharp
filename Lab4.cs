using System;
using System.Collections.Generic;

class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public bool IsAvailable { get; private set; }

    public Book(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
        IsAvailable = true;
    }

    public void MarkRented()
    {
        IsAvailable = false;
    }

    public void MarkReturned()
    {
        IsAvailable = true;
    }

    public override string ToString()
    {
        string status = IsAvailable ? "доступна" : "в оренді";
        return "[" + Id + "] " + Title + " - " + Author + " (" + status + ")";
    }
}

class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    private List<Book> rentedBooks = new List<Book>();

    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public List<Book> GetRentedBooks()
    {
        return new List<Book>(rentedBooks);
    }

    public bool HasBook(Book book)
    {
        return rentedBooks.Contains(book);
    }

    public void AddBook(Book book)
    {
        rentedBooks.Add(book);
    }

    public void RemoveBook(Book book)
    {
        rentedBooks.Remove(book);
    }

    public override string ToString()
    {
        string result = "[" + Id + "] " + Name + ", орендовані книги: ";
        if (rentedBooks.Count == 0)
        {
            result += "немає";
        }
        else
        {
            for (int i = 0; i < rentedBooks.Count; i++)
            {
                result += rentedBooks[i].Title;
                if (i < rentedBooks.Count - 1)
                {
                    result += ", ";
                }
            }
        }
        return result;
    }
}

class Library
{
    private List<Book> books = new List<Book>();
    private List<User> users = new List<User>();

    public void AddBook(Book book)
    {
        if (FindBook(book.Id) != null)
        {
            Console.WriteLine("Помилка: книга з id " + book.Id + " вже існує.");
            return;
        }
        books.Add(book);
    }

    public void AddUser(User user)
    {
        if (FindUser(user.Id) != null)
        {
            Console.WriteLine("Помилка: користувач з id " + user.Id + " вже існує.");
            return;
        }
        users.Add(user);
    }

    private Book FindBook(int id)
    {
        foreach (Book book in books)
        {
            if (book.Id == id)
            {
                return book;
            }
        }
        return null;
    }

    private User FindUser(int id)
    {
        foreach (User user in users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }
        return null;
    }

    public void RentBook(int userId, int bookId)
    {
        User user = FindUser(userId);
        if (user == null)
        {
            Console.WriteLine("Помилка: користувача з id " + userId + " не існує.");
            return;
        }

        Book book = FindBook(bookId);
        if (book == null)
        {
            Console.WriteLine("Помилка: книги з id " + bookId + " не існує.");
            return;
        }

        if (!book.IsAvailable)
        {
            Console.WriteLine("Помилка: книга \"" + book.Title + "\" вже в оренді.");
            return;
        }

        book.MarkRented();
        user.AddBook(book);
        Console.WriteLine(user.Name + " орендував книгу \"" + book.Title + "\".");
    }

    public void ReturnBook(int userId, int bookId)
    {
        User user = FindUser(userId);
        if (user == null)
        {
            Console.WriteLine("Помилка: користувача з id " + userId + " не існує.");
            return;
        }

        Book book = FindBook(bookId);
        if (book == null)
        {
            Console.WriteLine("Помилка: книги з id " + bookId + " не існує.");
            return;
        }

        if (!user.HasBook(book))
        {
            Console.WriteLine("Помилка: " + user.Name + " не орендував книгу \"" + book.Title + "\".");
            return;
        }

        user.RemoveBook(book);
        book.MarkReturned();
        Console.WriteLine(user.Name + " повернув книгу \"" + book.Title + "\".");
    }

    public void ShowAllBooks()
    {
        Console.WriteLine("Усі книги:");
        foreach (Book book in books)
        {
            Console.WriteLine(book);
        }
    }

    public void ShowAllUsers()
    {
        Console.WriteLine("Усі користувачі:");
        foreach (User user in users)
        {
            Console.WriteLine(user);
        }
    }

    public void ShowAvailableBooks()
    {
        Console.WriteLine("Доступні книги:");
        bool found = false;
        foreach (Book book in books)
        {
            if (book.IsAvailable)
            {
                Console.WriteLine(book);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Немає доступних книг.");
        }
    }

    public void ShowRentedBooks()
    {
        Console.WriteLine("Книги в оренді:");
        bool found = false;
        foreach (Book book in books)
        {
            if (!book.IsAvailable)
            {
                Console.WriteLine(book);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Немає книг в оренді.");
        }
    }

    public void CheckBookAvailability(int bookId)
    {
        Book book = FindBook(bookId);
        if (book == null)
        {
            Console.WriteLine("Помилка: книги з id " + bookId + " не існує.");
            return;
        }

        if (book.IsAvailable)
        {
            Console.WriteLine("Книга \"" + book.Title + "\" доступна.");
        }
        else
        {
            Console.WriteLine("Книга \"" + book.Title + "\" недоступна.");
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        library.AddBook(new Book(1, "Кобзар", "Тарас Шевченко"));
        library.AddBook(new Book(2, "Лісова пісня", "Леся Українка"));
        library.AddBook(new Book(3, "Тіні забутих предків", "Михайло Коцюбинський"));
        library.AddBook(new Book(4, "Захар Беркут", "Іван Франко"));

        library.AddUser(new User(1, "Олексій"));
        library.AddUser(new User(2, "Марія"));
        library.AddUser(new User(3, "Іван"));

        library.ShowAllBooks();
        Console.WriteLine();
        library.ShowAllUsers();
        Console.WriteLine();

        library.ShowAvailableBooks();
        Console.WriteLine();

        library.RentBook(1, 1);
        Console.WriteLine();

        library.RentBook(2, 1);
        Console.WriteLine();

        library.RentBook(2, 10);
        library.RentBook(10, 2);
        Console.WriteLine();

        library.ShowRentedBooks();
        Console.WriteLine();
        library.ShowAvailableBooks();
        Console.WriteLine();

        library.ReturnBook(2, 1);
        Console.WriteLine();

        library.ReturnBook(1, 1);
        Console.WriteLine();

        library.CheckBookAvailability(1);
        Console.WriteLine();

        library.ShowAllBooks();
        Console.WriteLine();
        library.ShowAllUsers();
    }
}
