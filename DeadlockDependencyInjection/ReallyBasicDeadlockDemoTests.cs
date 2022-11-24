using Microsoft.Extensions.DependencyInjection;

namespace DeadlockDependencyInjection
{
    public class ReallyBasicDeadlockDemoTests
    {
        private IServiceProvider _services = null!;
        
        [SetUp]
        public void Setup()
        {
            IServiceCollection container = new ServiceCollection();

            container.AddScoped<DeadlockDemoFactory>();
            container.AddScoped<IDeadlockDemoDependency, DeadlockDemoDependencyConcrete>();
            container.AddScoped(typeof(IDeadlockDemo), provider => DeadlockDemoFactoryOperator(provider)!);

            _services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
        }

        private IDeadlockDemo? DeadlockDemoFactoryOperator(IServiceProvider provider)
        {
            DeadlockDemoFactory factory = provider.GetRequiredService<DeadlockDemoFactory>();

            return factory.GetOneAsync().GetAwaiter().GetResult();
        }

        [Test]
        public void Deadlock()
        {
            using IServiceScope scope = _services.CreateScope();

            IDeadlockDemo demo = scope.ServiceProvider.GetRequiredService<IDeadlockDemo>();

            Assert.IsNotNull(demo);
        }
    }
}