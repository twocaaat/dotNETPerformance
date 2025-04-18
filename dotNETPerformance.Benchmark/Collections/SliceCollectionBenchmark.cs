using BenchmarkDotNet.Attributes;
using dotNETPerformance.Implementaion.Collections;

namespace dotNETPerformance.Benchmark.Collections;

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