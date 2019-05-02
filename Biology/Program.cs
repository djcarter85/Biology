namespace Biology
{
    using System;
    using System.Threading;
    using Biology.Core;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var creature = new Creature(birthProbabilityPerStep: 1, deathProbabilityPerCreaturePerStep: 0.1);

            var populationHistoryDistribution = new PopulationHistoryDistribution(creature, initialPopulation: 100);

            foreach (var population in populationHistoryDistribution.Sample())
            {
                Console.WriteLine(population);
                Thread.Sleep(100);
            }
        }
    }
}
