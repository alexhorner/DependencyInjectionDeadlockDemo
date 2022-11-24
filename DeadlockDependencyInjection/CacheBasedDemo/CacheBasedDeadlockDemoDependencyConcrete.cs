namespace DeadlockDependencyInjection.CacheBasedDemo
{
    public class CacheBasedDeadlockDemoDependencyConcrete : ICacheBasedDeadlockDemoDependency
    {
        public string GetId() => Guid.NewGuid().ToString();
    }
}