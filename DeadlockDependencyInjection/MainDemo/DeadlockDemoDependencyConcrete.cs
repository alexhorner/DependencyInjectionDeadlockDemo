namespace DeadlockDependencyInjection.MainDemo
{
    public class DeadlockDemoDependencyConcrete : IDeadlockDemoDependency
    {
        public string GetId() => Guid.NewGuid().ToString();
    }
}