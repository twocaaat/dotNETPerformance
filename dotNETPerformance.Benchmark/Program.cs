using BenchmarkDotNet.Running;
using dotNETPerformance.Benchmark.Collections;

// Collections
//BenchmarkRunner.Run<CreateBufferBenchmarks>();
//BenchmarkRunner.Run<SliceCollectionBenchmark>();
//BenchmarkRunner.Run<CopyCollectionBenchmarks>();
BenchmarkRunner.Run<IterativeFillCollectionBenchmarks>();
