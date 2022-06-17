using System;
using BenchmarkDotNet.Running;

namespace ZJCX.Cache.BenchmarkTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var benchmark = BenchmarkRunner.Run<MemorySpanTest>();
            Console.Read();
        }
    }
}
