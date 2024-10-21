public class BookFormatter
{
    private readonly Dictionary<string, string> _dictionary;
    private readonly int _wordsPerPage;

    public BookFormatter(Dictionary<string, string> dictionary, int wordsPerPage)
    {
        _dictionary = dictionary;
        _wordsPerPage = wordsPerPage;
    }

    public string FormatBook(string text)
    {
        var pages = text.Split(' ')
            .Select(word => _dictionary[word.ToLower()].ToUpper())
            .Select((word, i) => new { Word = word, Page = (i / _wordsPerPage) + 1 })
            .GroupBy(w => w.Page)
            .Select(g => string.Join(" ", g.Select(w => w.Word)));

        return string.Join("\n", pages);
    }
}