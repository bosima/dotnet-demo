public class TimeSortedQueue<TKey, TValue>
where TKey : notnull
where TValue : notnull
{
    Dictionary<TKey, List<TValue>> _data = new Dictionary<TKey, List<TValue>>();

    SortedList<DateTime, List<TKey>> _queue = new SortedList<DateTime, List<TKey>>();

    readonly object _lock = new object();

    readonly double _valueDequeueMillseconds;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueDequeueMillseconds">进入队列后的消息超过这个时间后出队列</param>
    public TimeSortedQueue(double valueDequeueMillseconds)
    {
        _valueDequeueMillseconds = valueDequeueMillseconds;
    }

    public void Publish(TKey key, TValue value)
    {
        DateTime now = DateTime.Now;
        lock (_lock)
        {
            if (_queue.TryGetValue(now, out List<TKey>? keys))
            {
                if (!keys!.Contains(key))
                {
                    keys.Add(key);
                }
            }
            else
            {
                _queue.Add(now, new List<TKey> { key });
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
    public double Consume(Func<(TKey Key, List<TValue> Value), bool> consumeFunction, int maxNumberOfMessages)
    {
        int consumeCount = 0;
        DateTime now = DateTime.Now;

        while (true)
        {
            lock (_lock)
            {
                if (!_queue.Any())
                {
                    return _valueDequeueMillseconds;
                }

                var first = _queue.First();
                var diffMillseconds = now.Subtract(first.Key).TotalMilliseconds;
                if (diffMillseconds < _valueDequeueMillseconds)
                {
                    return diffMillseconds;
                }

                var keys = first.Value;
                int processKeyCount = 0;
                foreach (var key in keys)
                {
                    bool? keyConsumeResult = null;
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
                    return 0;
                }
            }
        }
    }

    /// <summary>
    /// Pull a batch of messages.
    /// </summary>
    /// <param name="maxNumberOfMessages">The maximum number of pull messages.</param>
    /// <returns></returns>
    public List<(TKey key, List<TValue> value)> Pull(int maxNumberOfMessages)
    {
        List<(TKey, List<TValue>)> result = new List<(TKey, List<TValue>)>();
        DateTime now = DateTime.Now;

        lock (_lock)
        {
            int messageCount = 0;
            while (true)
            {
                if (!_queue.Any())
                {
                    break;
                }

                var first = _queue.First();
                var diffMillseconds = now.Subtract(first.Key).TotalMilliseconds;
                if (diffMillseconds < _valueDequeueMillseconds)
                {
                    break;
                }

                var keys = first.Value;
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