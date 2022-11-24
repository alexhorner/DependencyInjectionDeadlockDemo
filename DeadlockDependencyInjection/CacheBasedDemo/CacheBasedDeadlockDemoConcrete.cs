namespace DeadlockDependencyInjection.CacheBasedDemo
{
    public class CacheBasedDeadlockDemoConcrete : ICacheBasedDeadlockDemo
    {
        private readonly ICacheBasedDeadlockDemoDependency _dependency;

        public CacheBasedDeadlockDemoConcrete(ICacheBasedDeadlockDemoDependency dependency)
        {
            _dependency = dependency;
        }
        
        public string GetId() => Guid.NewGuid().ToString();

        public string GetDependencyId() => _dependency.GetId();
    }
}