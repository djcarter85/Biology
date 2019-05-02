namespace Biology.Core
{
    using Biology.Core.Randomness;
    using System.Collections.Generic;
    using System.Linq;

    public class LimitedPopulationHistoryDistribution : IDistribution<IReadOnlyList<IReadOnlyDictionary<CreatureType, int>>>
    {
        private readonly InfinitePopulationHistoryDistribution infinitePopulationHistoryDistribution;
        private readonly int steps;

        public LimitedPopulationHistoryDistribution(IReadOnlyList<Creature> creatures, int steps)
        {
            this.infinitePopulationHistoryDistribution = new InfinitePopulationHistoryDistribution(creatures);
            this.steps = steps;
        }

        public IReadOnlyList<IReadOnlyDictionary<CreatureType, int>> Sample()
        {
            return this.infinitePopulationHistoryDistribution.Sample()
                .Take(this.steps)
                .ToList();
        }
    }
}