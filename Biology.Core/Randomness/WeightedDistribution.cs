namespace Biology.Core.Randomness
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WeightedInteger : IDistribution<int>
    {
        private readonly IReadOnlyList<double> weights;
        private readonly List<double> cumulative;

        public WeightedInteger(IReadOnlyList<double> weights)
        {
            if (weights.Sum() != 1.0)
            {
                throw new ArgumentException();
            }

            this.weights = weights;
            this.cumulative = new List<double>(weights.Count);
            this.cumulative.Add(weights[0]);
            for (int i = 1; i < weights.Count; i++)
            {
                this.cumulative.Add(this.cumulative[i - 1] + weights[i]);
            }
        }

        public int Sample()
        {
            double uniform = StandardContinuousUniform.Distribution.Sample();

            int result = 0;
            while (this.cumulative[result] < uniform)
            {
                result++;
            }

            return result;
        }
    }
}