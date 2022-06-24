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
```

## SortedQueue

SortedQueue is a queue that can be sorted by any data.