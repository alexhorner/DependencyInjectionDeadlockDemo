using Microsoft.Extensions.DependencyInjection;

namespace DeadlockDependencyInjection
{
    public class DeadlockDemoFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DeadlockDemoFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IDeadlockDemo?> GetOneAsync()
        {
            //Do something

            await Task.Delay(1000);

            IDeadlockDemoDependency deadlockDemoDependency = _serviceProvider.GetRequiredService<IDeadlockDemoDependency>();

            return new DeadlockDemoConcrete(deadlockDemoDependency);
        }
    }
}