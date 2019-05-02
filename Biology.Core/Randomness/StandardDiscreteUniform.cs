namespace Biology.Core.Randomness
{
    using System;

    public sealed class StandardDiscreteUniform : IDistribution<int>
    {
        private StandardDiscreteUniform(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }

        public int Min { get; }

        public int Max { get; }

        public static StandardDiscreteUniform Distribution(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException();
            }

            return new StandardDiscreteUniform(min, max);
        }

        public int Sample() => (int)(StandardContinuousUniform.Distribution.Sample() * (1.0 + this.Max - this.Min)) + this.Min;
    }
}