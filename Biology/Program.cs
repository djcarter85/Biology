namespace Biology
{
    using System;
    using System.Threading;
    using Biology.Core;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var creature = new Creature(spontaneousBirthProbabilityPerStep: 0.1, deathProbabilityPerCreaturePerStep: 0.05, replicationProbabilityPerStep: 0.03);

            var populationHistoryDistribution = new PopulationHistoryDistribution(creature, initialPopulation: 0);

            foreach (var population in populationHistoryDistribution.Sample())
            {
                Console.WriteLine(population);
                Thread.Sleep(10);
            }
        }
    }
}
