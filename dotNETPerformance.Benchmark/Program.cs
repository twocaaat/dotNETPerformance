using BenchmarkDotNet.Running;
using dotNETPerformance.Benchmark;

var summary = BenchmarkRunner.Run<MockBenchmark>();