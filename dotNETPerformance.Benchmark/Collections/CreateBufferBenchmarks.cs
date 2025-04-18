using BenchmarkDotNet.Attributes;
using dotNETPerformance.Implementaion.Collections;

namespace dotNETPerformance.Benchmark.Collections;

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