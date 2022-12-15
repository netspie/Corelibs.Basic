using Common.Basic.Blocks;
using System.Threading.Tasks;

namespace Common.Basic.CQRS.Command
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TCommandOutput>
        where TCommand : ICommand
    {
        Task<Result<TCommandOutput>> Handle(TCommand command);
    }
}
