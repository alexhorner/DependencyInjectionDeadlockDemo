using DeadlockDependencyInjection.CacheBasedDemo;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DeadlockDependencyInjection
{
    public class CacheBasedDeadlockDemoTests
    {
        [Test]
        [Order(0)]
        public async Task NoDeadlockOnCacheBasedSingleton()
        {
            IServiceCollection container = new ServiceCollection();
            
            container.AddSingleton<ICacheBasedDeadlockDemoDependency, CacheBasedDeadlockDemoDependencyConcrete>();
            container.AddScoped<CacheBasedDeadlockDemoCache>();

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            CacheBasedDeadlockDemoCache demoCache = scope.ServiceProvider.GetRequiredService<CacheBasedDeadlockDemoCache>();

            ICacheBasedDeadlockDemo? demo = await demoCache.GetAsync();

            Assert.IsNotNull(demo);
        }
        
        [Test]
        [Order(1)]
        public async Task NoDeadlockOnCacheBasedTransient()
        {
            IServiceCollection container = new ServiceCollection();
            
            container.AddScoped<ICacheBasedDeadlockDemoDependency, CacheBasedDeadlockDemoDependencyConcrete>();
            container.AddScoped<CacheBasedDeadlockDemoCache>();

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            CacheBasedDeadlockDemoCache demoCache = scope.ServiceProvider.GetRequiredService<CacheBasedDeadlockDemoCache>();

            ICacheBasedDeadlockDemo? demo = await demoCache.GetAsync();

            Assert.IsNotNull(demo);
        }

        [Test]
        [Order(2)]
        public async Task NoDeadlockOnCacheBasedScoped()
        {
            IServiceCollection container = new ServiceCollection();
            
            container.AddScoped<ICacheBasedDeadlockDemoDependency, CacheBasedDeadlockDemoDependencyConcrete>();
            container.AddScoped<CacheBasedDeadlockDemoCache>();

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            CacheBasedDeadlockDemoCache demoCache = scope.ServiceProvider.GetRequiredService<CacheBasedDeadlockDemoCache>();

            ICacheBasedDeadlockDemo? demo = await demoCache.GetAsync();

            Assert.IsNotNull(demo);
        }
    }
}