// See https://aka.ms/new-console-template for more information

TimeSortedQueue<string, string> queue = new TimeSortedQueue<string, string>(3000);

List<Task> publishTasks = new List<Task>();

for (int i = 0; i < 4; i++)
{
    var j = i;
    publishTasks.Add(Task.Factory.StartNew(() =>
    {
        int k = 0;
        while (true)
        {
            queue.Publish($"key_{k}", $"value_{j}_{k}");
            Thread.Sleep(15);
            k++;
        }
    }, TaskCreationOptions.LongRunning));
}

Task.Factory.StartNew(() =>
{
    while (true)
    {
        var list = queue.Pull(100);
        if (list.Count <= 0)
        {
            Thread.Sleep(100);
            continue;
        }

        foreach (var item in list)
        {
            Console.WriteLine($"{DateTime.Now.ToString("mmss.fff")}:{item.key}, {string.Join(",", item.value)}");
        }
    }

}, TaskCreationOptions.LongRunning);

Task.Factory.StartNew(() =>
{
    while (true)
    {
        var wait = queue.Consume(message =>
            {
                Console.WriteLine(DateTime.Now.ToString("mmss.fff") + ":" + string.Join(",", message.Value));
                return true;
            }, 100);

        if (wait > 0)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(wait));
        }
        else
        {
            Thread.Sleep(100);
        }
    }

}, TaskCreationOptions.LongRunning);

Task.WaitAll(publishTasks.ToArray());

Console.ReadKey();