namespace Biology.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Biology.Core.Randomness;

    public class LimitedPopulationHistoryDistribution : IDistribution<IReadOnlyList<int>>
    {
        private readonly InfinitePopulationHistoryDistribution infinitePopulationHistoryDistribution;
        private readonly int steps;

        public LimitedPopulationHistoryDistribution(Creature creature, int initialPopulation, int steps)
        {
            this.infinitePopulationHistoryDistribution = new InfinitePopulationHistoryDistribution(creature, initialPopulation);
            this.steps = steps;
        }

        public IReadOnlyList<int> Sample()
        {
            return this.infinitePopulationHistoryDistribution.Sample()
                .Take(this.steps)
                .ToList();
        }
    }
}