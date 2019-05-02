namespace Biology
{
    using System;
    using Biology.Core;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var creature = new Creature(birthProbabilityPerStep: 1, deathProbabilityPerCreaturePerStep: 0.1);

            var populationHistoryDistribution = new PopulationHistoryDistribution(creature, initialPopulation: 100, steps: 200);

            do
            {
                var populations = populationHistoryDistribution.Sample();

                Console.WriteLine(string.Join(",", populations));
            } while (string.IsNullOrEmpty(Console.ReadLine()));
        }
    }
}
