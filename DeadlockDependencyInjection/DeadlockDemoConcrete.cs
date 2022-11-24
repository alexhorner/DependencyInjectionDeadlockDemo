namespace DeadlockDependencyInjection
{
    public class DeadlockDemoConcrete : IDeadlockDemo
    {
        private readonly IDeadlockDemoDependency _dependency;

        public DeadlockDemoConcrete(IDeadlockDemoDependency dependency)
        {
            _dependency = dependency;
        }
        
        public string GetId() => Guid.NewGuid().ToString();

        public string GetDependencyId() => _dependency.GetId();
    }
}