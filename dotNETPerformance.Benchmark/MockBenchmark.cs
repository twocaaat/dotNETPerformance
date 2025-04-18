using BenchmarkDotNet.Attributes;
using dotNETPerformance.Implementaion;

namespace dotNETPerformance.Benchmark;

[MemoryDiagnoser]
public class MockBenchmark
{
    [Params(10_000)]
    public int Iterations;
    
    [Benchmark]
    public void MyFirstBenchmarkMethod()
    {
        for (var i = 0; i < Iterations; i++)
        {
            MockImplementation.MockMethod(12345);
        }
    }
}