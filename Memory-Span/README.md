
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1741 (21H2)

Intel Core i5-10400F CPU 2.90GHz, 1 CPU, 12 logical and 6 physical cores

.NET SDK=6.0.300

```
  [Host]               : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT
  .NET 6.0             : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  .NET Core 3.1        : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT
  .NET Framework 4.7.2 : .NET Framework 4.8 (4.8.4510.0), X64 RyuJIT
```

|           Method |                  Job |              Runtime |     Mean |    Error |   StdDev |      Gen 0 | Allocated |
|----------------- |--------------------- |--------------------- |---------:|---------:|---------:|-----------:|----------:|
| TestNewByteArray |             .NET 6.0 |             .NET 6.0 | 39.02 ms | 0.452 ms | 0.401 ms | 25461.5385 |    153 MB |
|      TestNewSpan |             .NET 6.0 |             .NET 6.0 | 19.67 ms | 0.252 ms | 0.223 ms | 15281.2500 |     92 MB |
|   TestStackAlloc |             .NET 6.0 |             .NET 6.0 | 42.02 ms | 0.783 ms | 0.733 ms | 15230.7692 |     92 MB |
|      TestPointer |             .NET 6.0 |             .NET 6.0 | 23.25 ms | 0.342 ms | 0.303 ms | 15281.2500 |     92 MB |
| TestNewByteArray |        .NET Core 3.1 |        .NET Core 3.1 | 36.29 ms | 0.310 ms | 0.242 ms | 25500.0000 |    153 MB |
|      TestNewSpan |        .NET Core 3.1 |        .NET Core 3.1 | 22.62 ms | 0.228 ms | 0.203 ms | 15281.2500 |     92 MB |
|   TestStackAlloc |        .NET Core 3.1 |        .NET Core 3.1 | 46.19 ms | 0.686 ms | 0.641 ms | 15250.0000 |     92 MB |
|      TestPointer |        .NET Core 3.1 |        .NET Core 3.1 | 22.56 ms | 0.256 ms | 0.240 ms | 15281.2500 |     92 MB |
| TestNewByteArray | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 81.20 ms | 0.642 ms | 0.569 ms | 26714.2857 |    161 MB |
|      TestNewSpan | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 82.27 ms | 0.500 ms | 0.468 ms | 16571.4286 |     99 MB |
|   TestStackAlloc | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 85.98 ms | 0.457 ms | 0.357 ms | 16500.0000 |     99 MB |
|      TestPointer | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 64.07 ms | 0.442 ms | 0.392 ms | 16500.0000 |     99 MB |