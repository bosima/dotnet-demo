## TimeSortedQueue Demo

TimeSortedQueue is a queue sorted by time.

```
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
```

## SortedQueue

SortedQueue is a queue that can be sorted by any data.