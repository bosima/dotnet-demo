BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.3.1 (21E258) [Darwin 21.4.0]
Intel Core i5-8259U CPU 2.30GHz (Coffee Lake), 1 CPU, 4 logical and 4 physical cores

.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


|                  Method |       Mean |    Error |   StdDev |      Gen 0 |    Allocated |
|------------------------ |-----------:|---------:|---------:|-----------:|-------------:|
|               TestTuple | 6,916.8 us | 55.70 us | 52.10 us | 10195.3125 | 32,000,007 B |
|          TestValueTuple |   276.4 us |  0.97 us |  0.81 us |          - |            - |
| TestAnonymousValueTuple |   276.5 us |  1.66 us |  1.56 us |          - |            - |
|     TestNamedValueTuple |   275.9 us |  0.94 us |  0.83 us |          - |            - |


.NET SDK=6.0.101
  [Host]     : .NET Core 3.1.22 (CoreCLR 4.700.21.56803, CoreFX 4.700.21.57101), X64 RyuJIT
  DefaultJob : .NET Core 3.1.22 (CoreCLR 4.700.21.56803, CoreFX 4.700.21.57101), X64 RyuJIT


|                  Method |       Mean |    Error |   StdDev |      Gen 0 |    Allocated |
|------------------------ |-----------:|---------:|---------:|-----------:|-------------:|
|               TestTuple | 6,636.9 us | 90.66 us | 80.37 us | 10195.3125 | 32,000,076 B |
|          TestValueTuple |   827.3 us |  3.82 us |  3.57 us |          - |          1 B |
| TestAnonymousValueTuple |   552.4 us |  3.22 us |  2.69 us |          - |          1 B |
|     TestNamedValueTuple |   554.6 us |  3.66 us |  3.42 us |          - |          1 B |