using DeadlockDependencyInjection.WorkaroundDemo;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DeadlockDependencyInjection
{
    public class WorkaroundDeadlockDemoTests
    {
        private IWorkaroundDeadlockDemo? WorkaroundDeadlockDemoFactoryOperator(IServiceProvider provider)
        {
            WorkaroundDeadlockDemoFactory factory = provider.GetRequiredService<WorkaroundDeadlockDemoFactory>();

            return factory.GetOne();
        }
        
        [Test]
        [Order(0)]
        public void NoDeadlockOnWorkaroundSingleton()
        {
            IServiceCollection container = new ServiceCollection();

            container.AddSingleton<WorkaroundDeadlockDemoFactory>();
            container.AddSingleton<IWorkaroundDeadlockDemoDependency, WorkaroundDeadlockDemoDependencyConcrete>();
            container.AddSingleton(typeof(IWorkaroundDeadlockDemo), provider => WorkaroundDeadlockDemoFactoryOperator(provider)!);

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            IWorkaroundDeadlockDemo demo = scope.ServiceProvider.GetRequiredService<IWorkaroundDeadlockDemo>();

            Assert.IsNotNull(demo);
        }
        
        [Test]
        [Order(1)]
        public void NoDeadlockOnWorkaroundTransient()
        {
            IServiceCollection container = new ServiceCollection();

            container.AddScoped<WorkaroundDeadlockDemoFactory>();
            container.AddScoped<IWorkaroundDeadlockDemoDependency, WorkaroundDeadlockDemoDependencyConcrete>();
            container.AddTransient(typeof(IWorkaroundDeadlockDemo), provider => WorkaroundDeadlockDemoFactoryOperator(provider)!);

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            IWorkaroundDeadlockDemo demo = scope.ServiceProvider.GetRequiredService<IWorkaroundDeadlockDemo>();

            Assert.IsNotNull(demo);
        }

        [Test]
        [Order(2)]
        public void NoDeadlockOnWorkaroundScoped()
        {
            IServiceCollection container = new ServiceCollection();

            container.AddScoped<WorkaroundDeadlockDemoFactory>();
            container.AddScoped<IWorkaroundDeadlockDemoDependency, WorkaroundDeadlockDemoDependencyConcrete>();
            container.AddScoped(typeof(IWorkaroundDeadlockDemo), provider => WorkaroundDeadlockDemoFactoryOperator(provider)!);

            ServiceProvider services = container.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            
            using IServiceScope scope = services.CreateScope();

            IWorkaroundDeadlockDemo demo = scope.ServiceProvider.GetRequiredService<IWorkaroundDeadlockDemo>();

            Assert.IsNotNull(demo);
        }
    }
}