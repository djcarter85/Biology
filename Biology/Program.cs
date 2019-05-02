namespace Biology
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Biology.Core;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var creatures = new List<Creature>
            {
                CreatureBuilder.Create(CreatureType.Blue)
                    .WithSpontaneousBirthProbability(1)
                    .WithDeathProbabilityPerCreature(0.1)
                    .WithReplicationProbabilityPerCreature(0.05)
                    .WithMutationProbability(CreatureType.Green, 0.1)
                    .Build(),
                CreatureBuilder.Create(CreatureType.Green)
                    .WithSpontaneousBirthProbability(0)
                    .WithDeathProbabilityPerCreature(0.1)
                    .WithReplicationProbabilityPerCreature(0.05)
                    .Build(),
            };

            var populationHistoryDistribution = new InfinitePopulationHistoryDistribution(creatures);

            foreach (var populations in populationHistoryDistribution.Sample())
            {
                Console.WriteLine(string.Join(", ", populations.Select(kvp => $"{kvp.Key}: {kvp.Value:00}")));
                Thread.Sleep(10);
            }
        }
    }
}
