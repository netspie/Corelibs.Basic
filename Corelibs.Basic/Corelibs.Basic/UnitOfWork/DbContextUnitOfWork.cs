namespace Common.Basic.Common.Basic.UnitOfWork
{
    public class DbContextUnitOfWork : UnitOfWorkRegistry
    {
        public override Task Commit()
        {
            return Task.CompletedTask;
        }
    }
}
