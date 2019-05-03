namespace Biology.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreatureBuilder
    {
        private readonly CreatureType type;

        private readonly Dictionary<CreatureType, double> mutationProbabilities = new Dictionary<CreatureType, double>();

        private double spontaneousBirthProbabilityPerStep;
        private double deathProbabilityPerCreaturePerStep;
        private double crowdingCoefficient;
        private double replicationProbabilityPerCreaturePerStep;

        private CreatureBuilder(CreatureType type)
        {
            this.type = type;
        }

        public static CreatureBuilder Create(CreatureType type)
        {
            return new CreatureBuilder(type);
        }

        public CreatureBuilder WithSpontaneousBirthProbability(double spontaneousBirthProbability)
        {
            this.spontaneousBirthProbabilityPerStep = spontaneousBirthProbability;

            return this;
        }

        public CreatureBuilder WithDeathProbabilityPerCreature(double deathProbabilityPerCreature)
        {
            this.deathProbabilityPerCreaturePerStep = deathProbabilityPerCreature;

            return this;
        }

        public CreatureBuilder WithCrowdingCoefficient(double crowdingCoefficient)
        {
            this.crowdingCoefficient = crowdingCoefficient;

            return this;
        }

        public CreatureBuilder WithReplicationProbabilityPerCreature(double replicationProbabilityPerCreature)
        {
            this.replicationProbabilityPerCreaturePerStep = replicationProbabilityPerCreature;

            return this;
        }

        public CreatureBuilder WithMutationProbability(CreatureType creatureType, double mutationProbability)
        {
            if (creatureType == this.type)
            {
                throw new ArgumentException();
            }

            this.mutationProbabilities[creatureType] = mutationProbability;

            return this;
        }

        public Creature Build()
        {
            this.mutationProbabilities[this.type] = 1.0 - this.mutationProbabilities.Values.Sum();

            return new Creature(
                this.type,
                this.spontaneousBirthProbabilityPerStep,
                this.deathProbabilityPerCreaturePerStep,
                this.crowdingCoefficient,
                this.replicationProbabilityPerCreaturePerStep,
                this.mutationProbabilities);
        }
    }
}