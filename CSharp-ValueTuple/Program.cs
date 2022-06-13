// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

// demo
// var demo = new TupleDemo();
// demo.Run();

// benchmark
var benchmark = BenchmarkRunner.Run<TupleBenchmarkTest>();
Console.Read();


