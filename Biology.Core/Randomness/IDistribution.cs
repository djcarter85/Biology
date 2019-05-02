namespace Biology.Core.Randomness
{
    public interface IDistribution<T>
    {
        T Sample();
    }
}