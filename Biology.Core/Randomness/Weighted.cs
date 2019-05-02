namespace Biology.Core.Randomness
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Weighted<T> : IDistribution<T>
    {
        private readonly IReadOnlyDictionary<T, double> weights;

        public Weighted(IReadOnlyDictionary<T, double> weights)
        {
            if (weights.Values.Sum() != 1.0)
            {
                throw new ArgumentException();
            }

            this.weights = weights;
        }

        public T Sample()
        {
            var index = new WeightedInteger(this.weights.Values.ToList()).Sample();
            return this.weights.Keys.ElementAt(index);
        }
    }
}