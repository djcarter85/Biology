namespace Biology.Core.Randomness
{
    using System.Collections.Generic;

    public static class DistributionExtensions
    {
        public static IEnumerable<T> TakeSamples<T>(this IDistribution<T> distribution, int samples)
        {
            for (int i = 0; i < samples; i++)
            {
                yield return distribution.Sample();
            }
        }

        public static IDistribution<int> ToBinomial(this IDistribution<bool> bernoulli, int repetitions)
        {
            return new Binomial(bernoulli, repetitions);
        }
    }
}