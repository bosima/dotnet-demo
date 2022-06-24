/// <summary>
/// A Sortable Queue.
/// </summary>
/// <typeparam name="TSortKey"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class SortedQueue<TSortKey, TKey, TValue>
where TSortKey : notnull, IComparable
where TKey : notnull
where TValue : notnull
{
    Dictionary<TKey, List<TValue>> _data = new Dictionary<TKey, List<TValue>>();

    SortedList<TSortKey, List<TKey>> _queue = new SortedList<TSortKey, List<TKey>>();

    readonly object _lock = new object();

    /// <summary>
    /// Create a new instance of SortedQueue
    /// </summary>
    public SortedQueue(int maxNumberOfMessageConsumedOnce)
    {
    }

    /// <summary>
    /// Publish a message to queue
    /// </summary>
    /// <param name="sortKey">The key in the queue for sorting. Different messages can use the same key.</param>
    /// <param name="key">The message key.</param>
    /// <param name="value">The message value.</param>
    public void Publish(TSortKey sortKey, TKey key, TValue value)
    {
        lock (_lock)
        {
            if (_queue.TryGetValue(sortKey, out List<TKey>? keys))
            {
                keys.Add(key);
            }
            else
            {
                _queue.Add(sortKey, new List<TKey> { key });
            }

            if (_data.TryGetValue(key, out List<TValue>? values))
            {
                values.Add(value);
            }
            else
            {
                _data.Add(key, new List<TValue> { value });
            }
        }
    }

    /// <summary>
    /// Consume a batch of messages.
    /// </summary>
    /// <param name="consumeFunction">Function for consuming data.</param>
    /// <param name="maxNumberOfMessages">>Maximum number of messages consumed once.</param>
    /// <returns>Whether there is data unconsumed after this consumption.</returns>
    public bool Consume(Func<(TKey key, List<TValue> value), bool> consumeFunction, int maxNumberOfMessages)
    {
        int consumeCount = 0;
        bool remaining = false;
        while (true)
        {
            lock (_lock)
            {
                if (!_queue.Any())
                {
                    break;
                }

                var keys = _queue.First().Value;
                int processKeyCount = 0;
                foreach (var key in keys)
                {
                    bool? keyConsumeResult = false;
                    if (_data.TryGetValue(key, out List<TValue>? values))
                    {
                        keyConsumeResult = consumeFunction?.Invoke((key, values));
                    }

                     if (!keyConsumeResult.HasValue || keyConsumeResult.Value)
                    {
                        _data.Remove(key);
                        consumeCount += (values != null ? values.Count : 0);
                        processKeyCount++;
                    }
                }

                if (processKeyCount == keys.Count)
                {
                    _queue.RemoveAt(0);
                }

                if (consumeCount > maxNumberOfMessages)
                {
                    remaining = true;
                    break;
                }
            }
        }

        return remaining;
    }

    /// <summary>
    /// Pull a batch of messages.
    /// </summary>
    /// <param name="maxNumberOfMessages">The maximum number of pull messages.</param>
    /// <returns></returns>
    public List<(TKey Key, List<TValue> Value)> Pull(int maxNumberOfMessages)
    {
        List<(TKey, List<TValue>)> result = new List<(TKey, List<TValue>)>();
        lock (_lock)
        {
            int messageCount = 0;
            while (true)
            {
                if (!_queue.Any())
                {
                    break;
                }

                var keys = _queue.First().Value;
                foreach (var key in keys)
                {
                    if (_data.TryGetValue(key, out List<TValue>? keyValues))
                    {
                        result.Add((key, keyValues));
                        _data.Remove(key);
                        messageCount += keyValues!.Count;
                    }
                }
                _queue.RemoveAt(0);

                if (messageCount >= maxNumberOfMessages)
                {
                    break;
                }
            }
        }

        return result;
    }
}