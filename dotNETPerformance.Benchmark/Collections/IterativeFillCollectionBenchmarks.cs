using BenchmarkDotNet.Attributes;
using dotNETPerformance.Implementation.Collections;

namespace dotNETPerformance.Benchmark.Collections;

/*
| Method         | Iterations | Size | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0      | Gen1    | Allocated  | Alloc Ratio   |
|--------------- |----------- |----- |-----------:|----------:|----------:|------:|--------:|----------:|--------:|-----------:|--------------:|
| SpanBenchmark  | 10000      | 128  |   427.9 us |   6.62 us |   1.03 us |  1.00 |    0.00 |         - |       - |          - |            NA |
| ArrayBenchmark | 10000      | 128  |   545.7 us | 131.64 us |  34.19 us |  1.28 |    0.07 |  409.1797 |       - |  5360000 B |            NA |
| ListBenchmark  | 10000      | 128  | 1,880.5 us |  28.14 us |   4.36 us |  4.40 |    0.01 |  433.5938 |       - |  5680001 B |            NA |
|                |            |      |            |           |           |       |         |           |         |            |               |
| SpanBenchmark  | 10000      | 256  |   855.6 us |  26.66 us |   6.92 us |  1.00 |    0.01 |         - |       - |          - |            NA |
| ArrayBenchmark | 10000      | 256  | 1,101.9 us | 217.86 us |  33.71 us |  1.29 |    0.04 |  800.7813 |       - | 10480001 B |            NA |
| ListBenchmark  | 10000      | 256  | 1,571.4 us |  25.85 us |   4.00 us |  1.84 |    0.01 |  826.1719 |  1.9531 | 10800001 B |            NA |
|                |            |      |            |           |           |       |         |           |         |            |               |
| SpanBenchmark  | 10000      | 512  | 1,545.5 us |  23.69 us |   6.15 us |  1.00 |    0.01 |         - |       - |        1 B |          1.00 |
| ArrayBenchmark | 10000      | 512  | 1,847.6 us | 101.34 us |  26.32 us |  1.20 |    0.02 | 1583.9844 |       - | 20720001 B | 20,720,001.00 |
| ListBenchmark  | 10000      | 512  | 3,068.3 us |  99.05 us |  15.33 us |  1.99 |    0.01 | 1609.3750 | 11.7188 | 21040002 B | 21,040,002.00 |
|                |            |      |            |           |           |       |         |           |         |            |               |
| SpanBenchmark  | 10000      | 1024 | 2,936.0 us |  52.98 us |  13.76 us |  1.00 |    0.01 |         - |       - |        2 B |          1.00 |
| ArrayBenchmark | 10000      | 1024 | 3,513.1 us | 118.18 us |  18.29 us |  1.20 |    0.01 | 3152.3438 |       - | 41200002 B | 20,600,001.00 |
| ListBenchmark  | 10000      | 1024 | 6,372.4 us | 885.95 us | 230.08 us |  2.17 |    0.07 | 3171.8750 | 46.8750 | 41520003 B | 20,760,001.50 |
*/

[MemoryDiagnoser]
[WarmupCount(2)]
[IterationCount(5)]
public class IterativeFillCollectionBenchmarks
{
    [Params(10_000)]
    public int Iterations;
    
    [Params(128, 256, 512, 1024)]
    public int Size;

    [Benchmark(Baseline = true)]
    public void SpanBenchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            IterativeFillCollection.Span(Size);
        }
    }

    [Benchmark]
    public void ArrayBenchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            IterativeFillCollection.Array(Size);
        }
    }

    [Benchmark]
    public void ListBenchmark()
    {
        for (var i = 0; i < Iterations; i++)
        {
            IterativeFillCollection.List(Size);
        }
    }
}