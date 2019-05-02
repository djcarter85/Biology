namespace Biology.Core
{
    using System.Collections.Generic;
    using Biology.Core.Randomness;

    public class Creature
    {
        public Creature(
            CreatureType type,
            double spontaneousBirthProbabilityPerStep, 
            double deathProbabilityPerCreaturePerStep, 
            double replicationProbabilityPerStep, 
            IReadOnlyDictionary<CreatureType, double> mutationWeights)
        {
            this.Type = type;
            this.SpontaneousBirthDistribution = Bernoulli.Distribution(spontaneousBirthProbabilityPerStep);
            this.DeathDistribution = Bernoulli.Distribution(deathProbabilityPerCreaturePerStep);
            this.ReplicationDistribution = Bernoulli.Distribution(replicationProbabilityPerStep);
            this.MutationDistribution = new Weighted<CreatureType>(mutationWeights);
        }

        public CreatureType Type { get; }

        public IDistribution<bool> SpontaneousBirthDistribution { get; }

        public IDistribution<bool> DeathDistribution { get; }

        public IDistribution<bool> ReplicationDistribution { get; }

        public IDistribution<CreatureType> MutationDistribution { get; }
    }
}
