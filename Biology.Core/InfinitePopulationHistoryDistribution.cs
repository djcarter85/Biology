namespace Biology.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Biology.Core.Randomness;

    public class InfinitePopulationHistoryDistribution : IDistribution<IEnumerable<IReadOnlyDictionary<CreatureType, int>>>
    {
        private readonly IReadOnlyList<Creature> creatures;

        public InfinitePopulationHistoryDistribution(IReadOnlyList<Creature> creatures)
        {
            this.creatures = creatures;
        }

        public IEnumerable<IReadOnlyDictionary<CreatureType, int>> Sample()
        {
            IReadOnlyDictionary<CreatureType, int> currentPopulations = this.creatures.ToDictionary(c => c.Type, c => 0);

            while (true)
            {
                yield return currentPopulations;
                currentPopulations = new PopulationDistribution(this.creatures, currentPopulations).Sample();
            }
        }
    }
}