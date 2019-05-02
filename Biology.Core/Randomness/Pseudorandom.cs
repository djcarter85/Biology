namespace Biology.Core.Randomness
{
    using System;
    using System.Threading;

    public static class Pseudorandom
    {
        private static readonly ThreadLocal<Random> prng = new ThreadLocal<Random>(() => new Random(BetterRandom.NextInt()));

        public static double NextDouble() => prng.Value.NextDouble();
    }
}