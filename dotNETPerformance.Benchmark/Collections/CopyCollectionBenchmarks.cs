using BenchmarkDotNet.Attributes;
using dotNETPerformance.Implementation.Collections;

namespace dotNETPerformance.Benchmark.Collections;

/*
| Method                      | Size | Iterations | Mean       | Error     | StdDev   | Ratio | RatioSD | Gen0      | Gen1    | Allocated  | Alloc Ratio |
|---------------------------- |----- |----------- |-----------:|----------:|---------:|------:|--------:|----------:|--------:|-----------:|------------:|
| FromSpanBenchmark           | 512  | 10000      |   124.7 us |  33.94 us |  8.81 us |  1.00 |    0.09 |         - |       - |          - |          NA |
| FromArrayBenchmark          | 512  | 10000      |   281.0 us |   4.82 us |  1.25 us |  2.26 |    0.14 |  409.6680 |       - |  5360000 B |          NA |
| FromArrayBlockCopyBenchmark | 512  | 10000      |   285.8 us |   7.22 us |  1.12 us |  2.30 |    0.15 |  409.6680 |       - |  5360000 B |          NA |
| FromListBenchmark           | 512  | 10000      |   342.2 us |   5.51 us |  1.43 us |  2.76 |    0.17 |  434.5703 |  0.4883 |  5680000 B |          NA |
|                             |      |            |            |           |          |       |         |           |         |            |             |
| FromSpanBenchmark           | 2048 | 10000      |   495.3 us | 221.76 us | 57.59 us |  1.01 |    0.16 |         - |       - |          - |          NA |
| FromArrayBenchmark          | 2048 | 10000      | 1,073.0 us | 138.94 us | 21.50 us |  2.19 |    0.25 | 1583.9844 |       - | 20720001 B |          NA |
| FromArrayBlockCopyBenchmark | 2048 | 10000      | 1,166.9 us | 281.71 us | 73.16 us |  2.38 |    0.30 | 1583.9844 |       - | 20720001 B |          NA |
| FromListBenchmark           | 2048 | 10000      | 1,325.5 us | 338.48 us | 87.90 us |  2.71 |    0.34 | 1609.3750 | 11.7188 | 21040001 B |          NA |
 */

[WarmupCount(2)]
[IterationCount(5)]
[MemoryDiagnoser]
public class CopyCollectionBenchmarks
{
    [Params(512, 2048)]
    public int Size;
    
    [Params(10_000)]
    public int Iterations;

    private byte[] _array;
    private List<byte> _list;

    [GlobalSetup]
    public void Setup()
    {
        _array = new byte[Size];
        _list = new List<byte>(Size);
        for (int i = 0; i < Size; i++)
        {
            _array[i] = (byte)i;
            _list.Add((byte)i);
        }
    }
    
    [Benchmark(Baseline = true)]
    public void FromSpanBenchmark()
    {
        var span = _array.AsSpan();
        
        for (int i = 0; i < Iterations; i++)
        {
            CopyCollection.FromSpan(span, Size);
        }
    }
    
    [Benchmark]
    public void FromArrayBenchmark()
    {
        for (int i = 0; i < Iterations; i++)
        {
            CopyCollection.FromArray(_array, Size);
        }
    }
    
    [Benchmark]
    public void FromArrayBlockCopyBenchmark()
    {
        for (int i = 0; i < Iterations; i++)
        {
            CopyCollection.FromArrayBlockCopy(_array, Size);
        }
    }
    
    [Benchmark]
    public void FromListBenchmark()
    {
        for (int i = 0; i < Iterations; i++)
        {
            CopyCollection.FromList(_list);
        }
    }
}