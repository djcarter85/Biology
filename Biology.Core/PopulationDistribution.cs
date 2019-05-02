namespace Biology.Core
{
    using System.Linq;
    using Biology.Core.Randomness;

    public class PopulationDistribution : IDistribution<int>
    {
        private readonly Creature creature;
        private readonly int population;

        public PopulationDistribution(Creature creature, int population)
        {
            this.creature = creature;
            this.population = population;
        }

        public int Sample()
        {
            var spontaneousBirths = this.creature.SpontaneousBirthDistribution.Sample() ? 1 : 0;

            var replicationBirths = this.creature.ReplicationDistribution
                .TakeSamples(this.population)
                .Count(b => b);

            var deaths = this.creature.DeathDistribution
                .TakeSamples(this.population)
                .Count(b => b);

            return this.population + spontaneousBirths + replicationBirths - deaths;
        }
    }
}