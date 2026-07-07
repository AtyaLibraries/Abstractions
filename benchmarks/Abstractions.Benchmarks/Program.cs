using BenchmarkDotNet.Running;

namespace Abstractions.Benchmarks;

/// <summary>
/// Runs the Atya.Foundation.Abstractions benchmark suite.
/// </summary>
public static class Program
{
    /// <summary>
    /// Executes the benchmark suite.
    /// </summary>
    /// <param name="args">Command-line arguments passed to BenchmarkDotNet.</param>
    public static void Main(string[] args)
    {
        BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args, new BenchmarkConfig());
    }
}
