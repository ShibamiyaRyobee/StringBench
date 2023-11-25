using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace StringBench;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class ManyCharacters
{
    public static readonly int CharacterCount = 100000;

    [Benchmark]
    public string KusoBuilder()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < CharacterCount; i++)
        {
            // ここでCA1834
            sb.Append("あ");
        }
        return sb.ToString();
    }

    [Benchmark]
    public string BetterKusoBuilder()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < CharacterCount; i++)
        {
            sb.Append('あ');
        }
        return sb.ToString();
    }

    [Benchmark]
    public string Normal() => new('あ', CharacterCount);

    [Benchmark]
    public string Linq() => new(Enumerable.Repeat('あ', CharacterCount).ToArray());

    [Benchmark]
    public string Concat() => string.Concat(Enumerable.Repeat("あ", CharacterCount));
}
