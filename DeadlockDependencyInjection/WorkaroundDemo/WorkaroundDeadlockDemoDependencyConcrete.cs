namespace DeadlockDependencyInjection.WorkaroundDemo
{
    public class WorkaroundDeadlockDemoDependencyConcrete : IWorkaroundDeadlockDemoDependency
    {
        public string GetId() => Guid.NewGuid().ToString();
    }
}