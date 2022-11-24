namespace DeadlockDependencyInjection.WorkaroundDemo
{
    public class WorkaroundDeadlockDemoConcrete : IWorkaroundDeadlockDemo
    {
        private readonly IWorkaroundDeadlockDemoDependency _dependency;

        public WorkaroundDeadlockDemoConcrete(IWorkaroundDeadlockDemoDependency dependency)
        {
            _dependency = dependency;
        }
        
        public string GetId() => Guid.NewGuid().ToString();

        public string GetDependencyId() => _dependency.GetId();
    }
}