using System.Text;

var compressed = Compress("aaabbcccdde");
Console.WriteLine(compressed);

var decompressed = Decompress(compressed);
Console.WriteLine(decompressed);    

static string Compress(string input)
{
    var stringList = ConvertToList(input);
    var result = new StringBuilder();
    var skipCount = 0;

    while (skipCount < stringList.Count)
    {
        int count = AddRepeatingLetter(stringList.Skip(skipCount), result);

        if (count != 1)
            result.Append(count);
        skipCount += count;
    }

    return result.ToString();
}
static int AddRepeatingLetter(IEnumerable<char> nextPart, StringBuilder result)
{
    var first = nextPart.First();
    var tookPart = nextPart.TakeWhile(c => c == first);

    result.Append(first);
    return tookPart.Count();
}
static string Decompress(string input)
{
    var stringQueue = new Queue<char>(ConvertToList(input));
    var result = new StringBuilder();

    while (stringQueue.Any())
    {
        var letter = stringQueue.Dequeue();
        int count = DequeueRepeatingCount(stringQueue);

        result.Append(letter, count);
    }

    return result.ToString();
}

static List<char> ConvertToList(string input)
{
    if (input is null || input.Length == 0)
        throw new ArgumentNullException("String is empty");
    return input.ToLower().ToList();
}

static int DequeueRepeatingCount(Queue<char> stringQueue)
{
    var digitsLine = new StringBuilder();
    stringQueue
        .TakeWhile(c => int.TryParse(c.ToString(), out int _))
        .ToList()
        .ForEach(c =>
        {
            digitsLine.Append(c);
            stringQueue.Dequeue();
        });
    if (!int.TryParse(digitsLine.ToString(), out int count))
        count = 1;
    return count;
}