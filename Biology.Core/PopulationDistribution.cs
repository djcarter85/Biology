namespace Biology.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Biology.Core.Randomness;

    public class PopulationDistribution : IDistribution<IReadOnlyDictionary<CreatureType, int>>
    {
        private readonly IReadOnlyDictionary<CreatureType, Creature> creatures;
        private readonly IReadOnlyDictionary<CreatureType, int> initialPopulations;

        public PopulationDistribution(
            IReadOnlyDictionary<CreatureType, Creature> creatures,
            IReadOnlyDictionary<CreatureType, int> initialPopulations)
        {
            this.creatures = creatures;
            this.initialPopulations = initialPopulations;
        }

        public IReadOnlyDictionary<CreatureType, int> Sample()
        {
            var resultantPopulations = this.initialPopulations.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var keyValuePair in this.creatures)
            {
                var creatureType = keyValuePair.Key;
                var creature = keyValuePair.Value;
                var initialPopulation = this.initialPopulations[creatureType];

                var spontaneousBirths = creature.SpontaneousBirthDistribution.Sample() ? 1 : 0;

                var deaths = creature.DeathDistribution
                    .TakeSamples(initialPopulation)
                    .Count(b => b);

                resultantPopulations[creatureType] += spontaneousBirths - deaths;

                var replicationBirths = creature.ReplicationDistribution
                    .TakeSamples(initialPopulation)
                    .Count(b => b);

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