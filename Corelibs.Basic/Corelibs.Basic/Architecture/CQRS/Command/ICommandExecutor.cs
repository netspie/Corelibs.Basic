using Common.Basic.Blocks;

namespace Common.Basic.CQRS.Command
{
    public interface ICommandExecutor
    {
        Task<Result> Execute<TCommand>(TCommand command)
            where TCommand : ICommand;

        Task<Result<TCommandOutput>> Execute<TCommand, TCommandOutput>(TCommand command)
            where TCommand : ICommand;
    }
}
