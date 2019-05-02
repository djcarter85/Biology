namespace Biology.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Biology.Core.Randomness;

    public class LimitedPopulationHistoryDistribution : IDistribution<IReadOnlyList<IReadOnlyDictionary<CreatureType, int>>>
    {
        private readonly InfinitePopulationHistoryDistribution infinitePopulationHistoryDistribution;
        private readonly int steps;

        public LimitedPopulationHistoryDistribution(
            IReadOnlyList<Creature> creatures,
            IReadOnlyDictionary<CreatureType, int> initialPopulations,
            int steps)
        {
            this.infinitePopulationHistoryDistribution = new InfinitePopulationHistoryDistribution(creatures, initialPopulations);
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