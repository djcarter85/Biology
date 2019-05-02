namespace Biology.Core.Randomness
{
    public class Bernoulli : IDistribution<bool>
    {
        private readonly double occurrenceProbability;

        private Bernoulli(double occurrenceProbability)
        {
            this.occurrenceProbability = occurrenceProbability;
        }

        public static IDistribution<bool> Distribution(double occurrenceProbability) => new Bernoulli(occurrenceProbability);

        public bool Sample()
        {
            return StandardContinuousUniform.Distribution.Sample() < this.occurrenceProbability;
        }
    }
}