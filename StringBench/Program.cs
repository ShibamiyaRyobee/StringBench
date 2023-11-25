using BenchmarkDotNet.Running;

namespace StringBench;

internal class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<ManyCharacters>();
    }
}