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
            var creatures = new Dictionary<CreatureType, Creature>
            {
                {
                    CreatureType.Blue,
                    new Creature(
                        spontaneousBirthProbabilityPerStep: 1,
                        deathProbabilityPerCreaturePerStep: 0.1,
                        replicationProbabilityPerStep: 0.05,
                        new Dictionary<CreatureType, double> {{CreatureType.Blue, 0.9}, {CreatureType.Green, 0.1}})
                },
                {
                    CreatureType.Green,
                    new Creature(
                        spontaneousBirthProbabilityPerStep: 0,
                        deathProbabilityPerCreaturePerStep: 0.1,
                        replicationProbabilityPerStep: 0.05,
                        new Dictionary<CreatureType, double> {{CreatureType.Green, 1}})
                },
            };

            var initialPopulations = new Dictionary<CreatureType, int>
            {
                {CreatureType.Blue, 0},
                {CreatureType.Green, 0},
            };

            var populationHistoryDistribution = new InfinitePopulationHistoryDistribution(creatures, initialPopulations);

            foreach (var populations in populationHistoryDistribution.Sample())
            {
                Console.WriteLine(string.Join(", ", populations.Select(kvp => $"{kvp.Key}: {kvp.Value:00}")));
                Thread.Sleep(10);
            }
        }
    }
}
