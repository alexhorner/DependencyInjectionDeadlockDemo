using Microsoft.Extensions.DependencyInjection;

namespace DeadlockDependencyInjection
{
    public class ReallyBasicDeadlockDemoTests
    {
        private IDeadlockDemo? DeadlockDemoFactoryOperator(IServiceProvider provider)
        {
            DeadlockDemoFactory factory = provider.GetRequiredService<DeadlockDemoFactory>();

            return factory.GetOneAsync().GetAwaiter().GetResult();
        }
        
        [Test]
        [Order(0)]
        public void NoDeadlockOnSingleton()
        {
            IServiceCollection container = new ServiceCollection();

            container.AddSingleton<DeadlockDemoFactory>();
            container.AddSingleton<IDeadlockDemoDependency, DeadlockDemoDependencyConcrete>();
            container.AddSingleton(typeof(IDeadlockDemo), provider => DeadlockDemoFactoryOperator(provider)!);

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            IDeadlockDemo demo = scope.ServiceProvider.GetRequiredService<IDeadlockDemo>();

            Assert.IsNotNull(demo);
        }
        
        [Test]
        [Order(1)]
        public void NoDeadlockOnTransient()
        {
            IServiceCollection container = new ServiceCollection();

            container.AddScoped<DeadlockDemoFactory>();
            container.AddScoped<IDeadlockDemoDependency, DeadlockDemoDependencyConcrete>();
            container.AddTransient(typeof(IDeadlockDemo), provider => DeadlockDemoFactoryOperator(provider)!);

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            IDeadlockDemo demo = scope.ServiceProvider.GetRequiredService<IDeadlockDemo>();

            Assert.IsNotNull(demo);
        }

        [Test]
        [Order(2)]
        public void DeadlockOnScoped()
        {
            IServiceCollection container = new ServiceCollection();

            container.AddScoped<DeadlockDemoFactory>();
            container.AddScoped<IDeadlockDemoDependency, DeadlockDemoDependencyConcrete>();
            container.AddScoped(typeof(IDeadlockDemo), provider => DeadlockDemoFactoryOperator(provider)!);

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            IDeadlockDemo demo = scope.ServiceProvider.GetRequiredService<IDeadlockDemo>();

            Assert.IsNotNull(demo);
        }
    }
}