using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[SimpleJob]
public class TupleBenchmarkTest
{
    [GlobalSetup]
    public void GlobalSetup()
    {
    }

    [Benchmark]
    public void TestTuple()
    {
        for (int i = 0; i < 1000000; i++)
        {
            var t = GetTuple();
        }
    }

    [Benchmark]
    public void TestValueTuple()
    {
        for (int i = 0; i < 1000000; i++)
        {
            var t = GetValueTuple();
        }
    }

    private Tuple<int, string> GetTuple()
    {
        return Tuple.Create(100, "Hello Tuple Tuple Tuple!");
    }

    private ValueTuple<int, string> GetValueTuple()
    {
        return ValueTuple.Create(100, "Hello Tuple Tuple Tuple!");
    }
}