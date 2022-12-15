using Common.Basic.Blocks;
using Common.Basic.CQRS.Command;
using Common.Basic.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Basic.CQRS
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly ITypeMapper _typeMapper;
        private readonly Dictionary<string, Type> _dictionary = new Dictionary<string, Type>();

        public CommandExecutor(ITypeMapper typeMapper)
        {
            _typeMapper = typeMapper;
        }

        public void RegisterCommandHandler<TCommand, TCommandHandler>()
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>
        {
            _dictionary.Add(typeof(TCommand).Name, typeof(TCommandHandler));
        }

        public void RegisterCommandHandler<TCommand, TCommandHandler, TCommandOutput>()
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand, TCommandOutput>
        {
            _dictionary.Add(typeof(TCommand).Name, typeof(TCommandHandler));
        }

        Task<Result> ICommandExecutor.Execute<TCommand>(TCommand command)
        {
            var typeAndParameters = GetTypeAndParameters<TCommand>();
            var appCommandHandler = Activator.CreateInstance(typeAndParameters.Item1, typeAndParameters.Item2) as ICommandHandler<TCommand>;
            return appCommandHandler.Handle(command);
        }

        Task<Result<TCommandOutput>> ICommandExecutor.Execute<TCommand, TCommandOutput>(TCommand command)
        {
            var typeAndParameters = GetTypeAndParameters<TCommand>();
            var appCommandHandler = Activator.CreateInstance(typeAndParameters.Item1, typeAndParameters.Item2) as ICommandHandler<TCommand, TCommandOutput>;
            return appCommandHandler.Handle(command);
        }

        private (Type, object[]) GetTypeAndParameters<TCommand>()
        {
            Type type = GetCommandHandlerType<TCommand>();
            if (type == null)
                return default;

            var constructors = type.GetConstructors();
            var parametersArrays = constructors.Select(c => c.GetParameters()).ToArray();
            var parameters = parametersArrays.SelectMany(p => p).ToArray();
            var parametersMapped = parameters.Select(p => _typeMapper.Get(p.ParameterType)).ToArray();

            return (type, parametersMapped);
        }

        private Type GetCommandHandlerType<TCommand>()
        {
            var typeName = typeof(TCommand).Name;
            _dictionary.TryGetValue(typeName, out var value);
            return value;
        }
    }
}
