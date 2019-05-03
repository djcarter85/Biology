namespace Biology.Core.Randomness
{
    using System.Linq;

    public class Binomial : IDistribution<int>
    {
        private readonly IDistribution<bool> bernoulli;
        private readonly int repetitions;

        public Binomial(IDistribution<bool> bernoulli, int repetitions)
        {
            this.bernoulli = bernoulli;
            this.repetitions = repetitions;
        }

        public int Sample()
        {
            return this.bernoulli
                .TakeSamples(this.repetitions)
                .Count(b => b);
        }
    }
}