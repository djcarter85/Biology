namespace Biology.Core
{
    using System.Collections.Generic;
    using Biology.Core.Randomness;

    public class InfinitePopulationHistoryDistribution : IDistribution<IEnumerable<IReadOnlyDictionary<CreatureType, int>>>
    {
        private readonly IReadOnlyList<Creature> creatures;
        private readonly IReadOnlyDictionary<CreatureType, int> initialPopulations;

        public InfinitePopulationHistoryDistribution(
            IReadOnlyList<Creature> creatures,
            IReadOnlyDictionary<CreatureType, int> initialPopulations)
        {
            this.creatures = creatures;
            this.initialPopulations = initialPopulations;
        }

        public IEnumerable<IReadOnlyDictionary<CreatureType, int>> Sample()
        {
            var currentPopulations = this.initialPopulations;

            while (true)
            {
                yield return currentPopulations;
                currentPopulations = new PopulationDistribution(this.creatures, currentPopulations).Sample();
            }
        }
    }
}