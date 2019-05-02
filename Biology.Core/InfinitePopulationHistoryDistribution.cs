namespace Biology.Core
{
    using System.Collections.Generic;
    using Biology.Core.Randomness;

    public class InfinitePopulationHistoryDistribution : IDistribution<IEnumerable<int>>
    {
        private readonly Creature creature;
        private readonly int initialPopulation;

        public InfinitePopulationHistoryDistribution(Creature creature, int initialPopulation)
        {
            this.creature = creature;
            this.initialPopulation = initialPopulation;
        }

        public IEnumerable<int> Sample()
        {
            var currentPopulation = this.initialPopulation;

            while (true)
            {
                yield return currentPopulation;
                currentPopulation = new PopulationDistribution(this.creature, currentPopulation).Sample();
            }
        }
    }
}