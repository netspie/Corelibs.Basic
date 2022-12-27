using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Corelibs.Basic.Repository
{
    public class DbContextTransactionBehaviour<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly DbContext _context;

        public DbContextTransactionBehaviour(DbContext context)
        {
            _context = context;
        }

        public async ValueTask<TResponse> Handle(TCommand command, CancellationToken ct, MessageHandlerDelegate<TCommand, TResponse> next)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var response = await next(command, ct);

                    await transaction.CommitAsync();

                    return response;
                }
                catch (Exception ex) 
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine(ex.ToString());

                    return default;
                }
            }
        }
    }
}
