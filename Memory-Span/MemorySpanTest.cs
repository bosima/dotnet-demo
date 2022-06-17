using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

#if NET472
using System.Text;
#endif

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.NetCoreApp31)]
[SimpleJob(RuntimeMoniker.Net472)]
[SimpleJob(RuntimeMoniker.Net60)]
public class MemorySpanTest
{
    byte[] data = { };

    [GlobalSetup]
    public void GlobalSetup()
    {
        var id = Guid.NewGuid().ToString();
        data = System.Text.Encoding.ASCII.GetBytes($"{id}:huhahahuhahahuhahahadididididihuhahahuhahahuhahahadididididi-openthedoor");
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        data = new byte[0];
    }

    [Benchmark]
    public void TestNewByteArray()
    {
        for (int i = 0; i < 1000000; i++)
        {
            ProcessDataWithNewByteArray(data);
        }
    }

    [Benchmark]
    public void TestNewSpan()
    {
        for (int i = 0; i < 1000000; i++)
        {
            ProcessDataWithNewSpan(data);
        }
    }

    [Benchmark]
    public void TestStackAlloc()
    {
        for (int i = 0; i < 1000000; i++)
        {
            ProcessDataWithStackAlloc(data);
        }
    }

    [Benchmark]
    public void TestPointer()
    {
        for (int i = 0; i < 1000000; i++)
        {
            ProcessDataWithPointer(data);
        }
    }

    public void ProcessDataWithNewByteArray(byte[] data)
    {
        var strLen = 36;
        byte[] strBytes = new byte[strLen];
        Buffer.BlockCopy(data, 0, strBytes, 0, strLen);
        var str = System.Text.Encoding.ASCII.GetString(strBytes);
        DoSomething(str);
    }

    public void ProcessDataWithNewSpan(byte[] data)
    {
        var strLen = 36;
        var span = new Span<byte>(data, 0, strLen);
        var str = System.Text.Encoding.ASCII.GetString(span);
        DoSomething(str);
    }

    public void ProcessDataWithStackAlloc(byte[] data)
    {
        var strLen = 36;
        string str;

        unsafe
        {
            var strBytes = stackalloc byte[strLen];
            for (int i = 0; i < strLen; i++)
            {
                strBytes[i] = data[i];
            }

            str = System.Text.Encoding.ASCII.GetString(strBytes, strLen);
        }

        DoSomething(str);
    }

    public void ProcessDataWithPointer(byte[] data)
    {
        var strLen = 36;
        string str;

        unsafe
        {
            fixed (byte* strBytes = data)
            {
                str = System.Text.Encoding.ASCII.GetString(strBytes, strLen);
            }
        }

        DoSomething(str);
    }

    private void DoSomething(string str)
    {
        //Console.WriteLine(str);
    }
}