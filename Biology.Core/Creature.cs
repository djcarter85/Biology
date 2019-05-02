namespace Biology.Core
{
    using Biology.Core.Randomness;

    public class Creature
    {
        public Creature(double spontaneousBirthProbabilityPerStep, double deathProbabilityPerCreaturePerStep, double replicationProbabilityPerStep)
        {
            this.SpontaneousBirthDistribution = Bernoulli.Distribution(spontaneousBirthProbabilityPerStep);
            this.DeathDistribution = Bernoulli.Distribution(deathProbabilityPerCreaturePerStep);
            this.ReplicationDistribution = Bernoulli.Distribution(replicationProbabilityPerStep);
        }

        public IDistribution<bool> SpontaneousBirthDistribution { get; }

        public IDistribution<bool> DeathDistribution { get; }

        public IDistribution<bool> ReplicationDistribution { get; }
    }
}
