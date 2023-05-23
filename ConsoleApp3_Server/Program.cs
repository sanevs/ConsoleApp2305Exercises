var tasks = new List<Task>();

for (int i = 1; i < 1000; i++)
{
    int val = i;
    tasks.Add(Task.Run(async () => await Server.GetCount()));
    tasks.Add(Task.Run(() => Server.AddToCount(val)));
}

await Task.WhenAll(tasks);
Console.WriteLine($"Total: {await Server.GetCount()}");
Console.WriteLine("All tasks completed!");

public static class Server
{
    private static object locker = new object();
    private static bool isEntered = false;
    private static int count = 0;

    public static async Task<int> GetCount()
    {
        while (isEntered)
            await Task.Delay(10);

        return count;
    }
    public static void AddToCount(int count)
    {
        lock (locker)
        {
            isEntered= true;
            Server.count += count;
            isEntered = false;
        }
    }
}