using BenchmarkDotNet.Attributes;
using dotNETPerformance.Implementation.Collections;

namespace dotNETPerformance.Benchmark.Collections;

/*
| Method             | Iterations | Size | Mean        | Error      | StdDev    | Ratio | RatioSD | Gen0      | Gen1    | Allocated  | Alloc Ratio |
|------------------- |----------- |----- |------------:|-----------:|----------:|------:|--------:|----------:|--------:|-----------:|------------:|
| FromSpanBenchmark  | 10000      | 128  |    64.90 us |   1.516 us |  0.394 us |  1.00 |    0.01 |         - |       - |          - |          NA |
| FromArrayBenchmark | 10000      | 128  |    91.21 us |  22.562 us |  5.859 us |  1.41 |    0.08 |  116.2109 |       - |  1520000 B |          NA |
| FromListBenchmark  | 10000      | 128  |   288.15 us |  15.349 us |  3.986 us |  4.44 |    0.06 |  458.9844 |  0.9766 |  6000000 B |          NA |
|                    |            |      |             |            |           |       |         |           |         |            |             |
| FromSpanBenchmark  | 10000      | 256  |    81.70 us |   5.307 us |  0.821 us |  1.00 |    0.01 |         - |       - |          - |          NA |
| FromArrayBenchmark | 10000      | 256  |   125.30 us |  16.798 us |  2.599 us |  1.53 |    0.03 |  214.2334 |       - |  2800000 B |          NA |
| FromListBenchmark  | 10000      | 256  |   511.91 us | 184.634 us | 28.572 us |  6.27 |    0.32 |  850.5859 |  3.4180 | 11120000 B |          NA |
|                    |            |      |             |            |           |       |         |           |         |            |             |
| FromSpanBenchmark  | 10000      | 512  |   117.78 us |   3.548 us |  0.549 us |  1.00 |    0.01 |         - |       - |          - |          NA |
| FromArrayBenchmark | 10000      | 512  |   189.32 us |  11.249 us |  2.921 us |  1.61 |    0.02 |  409.9121 |       - |  5360000 B |          NA |
| FromListBenchmark  | 10000      | 512  |   850.57 us |  29.282 us |  7.604 us |  7.22 |    0.07 | 1634.7656 | 12.6953 | 21360000 B |          NA |
|                    |            |      |             |            |           |       |         |           |         |            |             |
| FromSpanBenchmark  | 10000      | 1024 |   218.64 us |   8.615 us |  2.237 us |  1.00 |    0.01 |         - |       - |          - |          NA |
| FromArrayBenchmark | 10000      | 1024 |   373.88 us |  23.579 us |  6.123 us |  1.71 |    0.03 |  801.7578 |       - | 10480000 B |          NA |
| FromListBenchmark  | 10000      | 1024 | 1,680.74 us | 191.512 us | 49.735 us |  7.69 |    0.22 | 3201.1719 |       - | 41840001 B |          NA |
*/

[MemoryDiagnoser]
[WarmupCount(2)]
[IterationCount(5)]
public class CreateBufferBenchmarks
{
    [Params(10_000)]
    public int Iterations;
    
    [Params(128, 256, 512, 1024)]
    public int Size;

    [Benchmark(Baseline = true)]
    public void FromSpanBenchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            CreateBuffer.FromSpan(Size);
        }
    }

    [Benchmark]
    public void FromArrayBenchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            CreateBuffer.FromArray(Size);
        }
    }

    [Benchmark]
    public void FromListBenchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            CreateBuffer.FromList(Size);
        }
    }
}