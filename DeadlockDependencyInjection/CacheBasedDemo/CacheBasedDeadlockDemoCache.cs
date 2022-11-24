using Microsoft.Extensions.DependencyInjection;

namespace DeadlockDependencyInjection.CacheBasedDemo
{
    public class CacheBasedDeadlockDemoCache
    {
        private readonly IServiceProvider _services;
        private ICacheBasedDeadlockDemo? _cacheBasedDeadlockDemo;

        public CacheBasedDeadlockDemoCache(IServiceProvider services)
        {
            _services = services;
        }

        public async Task<ICacheBasedDeadlockDemo?> GetAsync()
        {
            _cacheBasedDeadlockDemo ??= await GetIfNotPresentAsync();

            return _cacheBasedDeadlockDemo;
        }

        private async Task<ICacheBasedDeadlockDemo?> GetIfNotPresentAsync()
        {
            //Do something, anything, which needs an await
            //Our example "something" here is a delay, but in production code this was first discovered with an async HTTP request
            await Task.Delay(1000);

            ICacheBasedDeadlockDemoDependency deadlockDemoDependency = _services.GetRequiredService<ICacheBasedDeadlockDemoDependency>();

            return new CacheBasedDeadlockDemoConcrete(deadlockDemoDependency);
        }
    }
}