namespace Biology.Core
{
    using System.Collections.Generic;
    using Biology.Core.Randomness;

    public class Creature
    {
        private readonly double deathProbabilityPerCreaturePerStep;
        private readonly double crowdingCoefficient;

        public Creature(
            CreatureType type,
            double spontaneousBirthProbabilityPerStep, 
            double deathProbabilityPerCreaturePerStep, 
            double crowdingCoefficient,
            double replicationProbabilityPerStep, 
            IReadOnlyDictionary<CreatureType, double> mutationWeights)
        {
            this.deathProbabilityPerCreaturePerStep = deathProbabilityPerCreaturePerStep;
            this.crowdingCoefficient = crowdingCoefficient;
            this.Type = type;
            this.SpontaneousBirthDistribution = Bernoulli.Distribution(spontaneousBirthProbabilityPerStep);
            this.ReplicationDistribution = Bernoulli.Distribution(replicationProbabilityPerStep);
            this.MutationDistribution = new Weighted<CreatureType>(mutationWeights);
        }

        public CreatureType Type { get; }

        public IDistribution<bool> SpontaneousBirthDistribution { get; }

        public IDistribution<bool> DeathDistribution(int currentPopulation) => 
            Bernoulli.Distribution(this.deathProbabilityPerCreaturePerStep + this.crowdingCoefficient * currentPopulation);

        public IDistribution<bool> ReplicationDistribution { get; }

        public IDistribution<CreatureType> MutationDistribution { get; }
    }
}
