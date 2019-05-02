namespace Biology.Core
{
    using System.Collections.Generic;
    using Biology.Core.Randomness;

    public class PopulationHistoryDistribution : IDistribution<IEnumerable<int>>
    {
        private readonly Creature creature;
        private readonly int initialPopulation;
        private readonly int steps;

        public PopulationHistoryDistribution(Creature creature, int initialPopulation, int steps)
        {
            this.creature = creature;
            this.initialPopulation = initialPopulation;
            this.steps = steps;
        }

        public IEnumerable<int> Sample()
        {
            var currentPopulation = this.initialPopulation;

            for (int i = 0; i < this.steps; i++)
            {
                yield return currentPopulation;
                currentPopulation = new PopulationDistribution(this.creature, currentPopulation).Sample();
            }
        }
    }
}