namespace Biology.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Biology.Core.Randomness;

    public class PopulationDistribution : IDistribution<IReadOnlyDictionary<CreatureType, int>>
    {
        private readonly IReadOnlyList<Creature> creatures;
        private readonly IReadOnlyDictionary<CreatureType, int> initialPopulations;

        public PopulationDistribution(
            IReadOnlyList<Creature> creatures,
            IReadOnlyDictionary<CreatureType, int> initialPopulations)
        {
            this.creatures = creatures;
            this.initialPopulations = initialPopulations;
        }

        public IReadOnlyDictionary<CreatureType, int> Sample()
        {
            var resultantPopulations = this.initialPopulations.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var creature in this.creatures)
            {
                var initialPopulation = this.initialPopulations[creature.Type];

                var spontaneousBirths = creature.SpontaneousBirthDistribution.Sample() ? 1 : 0;

                var deaths = creature.DeathDistribution(initialPopulation)
                    .ToBinomial(initialPopulation)
                    .Sample();

                resultantPopulations[creature.Type] += spontaneousBirths - deaths;

                var replicationBirths = creature.ReplicationDistribution
                    .ToBinomial(initialPopulation)
                    .Sample();

                var groupBy = creature.MutationDistribution
                    .TakeSamples(replicationBirths)
                    .GroupBy(ct => ct);

                foreach (var grouping in groupBy)
                {
                    resultantPopulations[grouping.Key] += grouping.Count();
                }
            }

            return resultantPopulations;
        }
    }
}