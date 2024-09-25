using System.Text.RegularExpressions;

namespace BookClass;

public class Book : IComparable<Book>, IParsable<Book>
{
    public string LastName { get; private set; }
    public string FirstName { get; private set; }
    public string Title { get; private set; }
    public DateTime ReleaseDate { get; private set; }


    public static Book Parse(string input, IFormatProvider? provider)
    {
        string pattern = @"\|\s*(\w+)\s*\|\s*(\w+)\s*\|\s*([^\|]+?)\s*\|\s*([\d-]+)\s*\|"; //I asked ChatGPT to give me this pattern after like 20 minutes of trying to figure out the RegEx myself

        Match match = Regex.Match(input, pattern);

        Book parsedBook = new Book(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, DateTime.Parse(match.Groups[4].Value));

        return parsedBook;
    }

    public static bool TryParse(string input, IFormatProvider? provider, out Book book)
    {
        string pattern = @"\|\s*(\w+)\s*\|\s*(\w+)\s*\|\s*([^\|]+?)\s*\|\s*([\d-]+)\s*\|";

        book = null;

        if (!Regex.IsMatch(input, pattern))
        {
            return false;
        }

        try
        {
            book = Book.Parse(input);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception {e} - Message: {e.Message}");
            return false;
        }
    }

    public static Book Parse(string input)
    {
        return Parse(input, null);
    }

    public static bool TryParse(string input, out Book book)
    {
        return TryParse(input, null, out book);
    }


    public int CompareTo(Book other)
    {
        if (this.LastName != other.LastName)
        {
            return this.LastName.CompareTo(other.LastName);
        }
        if (this.FirstName != other.FirstName)
        {
            return this.FirstName.CompareTo(other.FirstName);
        }
        if (this.Title != other.Title)
        {
            return this.Title.CompareTo(other.Title);
        }
        if (this.ReleaseDate != other.ReleaseDate)
        {
            return this.ReleaseDate.CompareTo(other.ReleaseDate);
        }

        return 0;
    }

    public override string ToString()
    {
        return $"{this.LastName},{this.FirstName}, \"{this.Title}\", {this.ReleaseDate.ToShortDateString()}";
    }

    public Book(string lName, string fName, string bTitle, DateTime rDate)
    {
        this.LastName = lName;
        this.FirstName = fName;
        this.Title = bTitle;
        this.ReleaseDate = rDate;
    }

    public Book() : this("", "", "", new DateTime())
    {
    }
}
