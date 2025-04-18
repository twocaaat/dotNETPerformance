using BenchmarkDotNet.Attributes;
using dotNETPerformance.Implementation.Collections;

namespace dotNETPerformance.Benchmark.Collections;

/*
| Method                 | Size | Iterations | Mean       | Error      | StdDev    | Ratio  | RatioSD | Gen0     | Gen1   | Allocated | Alloc Ratio |
|----------------------- |----- |----------- |-----------:|-----------:|----------:|-------:|--------:|---------:|-------:|----------:|------------:|
| SpanSliceBenchmark     | 512  | 10000      |   2.187 us |  0.0146 us | 0.0038 us |   1.00 |    0.00 |        - |      - |         - |          NA |
| ArraySliceBenchmark    | 512  | 10000      |   4.355 us |  0.0562 us | 0.0146 us |   1.99 |    0.01 |        - |      - |         - |          NA |
| ListSliceBenchmark     | 512  | 10000      | 267.258 us | 15.4829 us | 4.0209 us | 122.18 |    1.69 | 348.6328 | 0.4883 | 4560000 B |          NA |
| ListSpanSliceBenchmark | 512  | 10000      |   5.440 us |  0.1145 us | 0.0297 us |   2.49 |    0.01 |        - |      - |         - |          NA |
|                        |      |            |            |            |           |        |         |          |        |           |             |
| SpanSliceBenchmark     | 2048 | 10000      |   2.158 us |  0.0159 us | 0.0025 us |   1.00 |    0.00 |        - |      - |         - |          NA |
| ArraySliceBenchmark    | 2048 | 10000      |   4.315 us |  0.0318 us | 0.0083 us |   2.00 |    0.00 |        - |      - |         - |          NA |
| ListSliceBenchmark     | 2048 | 10000      | 260.119 us |  4.1066 us | 1.0665 us | 120.56 |    0.47 | 348.6328 | 0.4883 | 4560000 B |          NA |
| ListSpanSliceBenchmark | 2048 | 10000      |   5.399 us |  0.0975 us | 0.0151 us |   2.50 |    0.01 |        - |      - |         - |          NA |
*/

[WarmupCount(2)]
[IterationCount(5)]
[MemoryDiagnoser]
public class SliceCollectionBenchmark
{
    [Params(512, 2048)]
    public int Size;
    
    [Params(10_000)]
    public int Iterations;

    private int[] _array;
    private List<int> _list;

    private int _start = 100;
    private int _length = 100;

    [GlobalSetup]
    public void Setup()
    {
        _array = new int[Size];
        _list = new List<int>(Size);
        for (int i = 0; i < Size; i++)
        {
            _array[i] = i;
            _list.Add(i);
        }
    }

    [Benchmark(Baseline = true)]
    public void SpanSliceBenchmark()
    {
        var span = _array.AsSpan();
        
        for (int i = 0; i < Iterations; i++)
        {
            SliceCollection.SliceSpan(span, _start, _length);
        }
    }

    [Benchmark]
    public void ArraySliceBenchmark()
    {
        for (int i = 0; i < Iterations; i++)
        {
            SliceCollection.SliceArray(_array, _start, _length);
        }
    }

    [Benchmark]
    public void ListSliceBenchmark()
    {
        for (int i = 0; i < Iterations; i++)
        {
            SliceCollection.SliceList(_list, _start, _length);
        }
    }

    [Benchmark]
    public void ListSpanSliceBenchmark()
    {
        for (int i = 0; i < Iterations; i++)
        {
            SliceCollection.SliceListAsSpan(_list, _start, _length);
        }
    }
}
