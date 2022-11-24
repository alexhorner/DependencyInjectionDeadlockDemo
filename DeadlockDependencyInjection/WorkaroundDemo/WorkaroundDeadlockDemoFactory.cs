using Microsoft.Extensions.DependencyInjection;

namespace DeadlockDependencyInjection.WorkaroundDemo
{
    public class WorkaroundDeadlockDemoFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public WorkaroundDeadlockDemoFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IWorkaroundDeadlockDemo? GetOne()
        {
            //We will now work around the issue by ensuring all calls are non-async on this level. They can be async further down the line though
            Task.Delay(1000).GetAwaiter().GetResult();

            IWorkaroundDeadlockDemoDependency deadlockDemoDependency = _serviceProvider.GetRequiredService<IWorkaroundDeadlockDemoDependency>();

            return new WorkaroundDeadlockDemoConcrete(deadlockDemoDependency);
        }
    }
}