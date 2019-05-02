namespace Biology.Core
{
    using Biology.Core.Randomness;

    public class Creature
    {
        public Creature(double birthProbabilityPerStep, double deathProbabilityPerCreaturePerStep)
        {
            this.BirthDistribution = Bernoulli.Distribution(birthProbabilityPerStep);
            this.DeathDistribution = Bernoulli.Distribution(deathProbabilityPerCreaturePerStep);
        }

        public IDistribution<bool> BirthDistribution { get; }

        public IDistribution<bool> DeathDistribution { get; }
    }
}
