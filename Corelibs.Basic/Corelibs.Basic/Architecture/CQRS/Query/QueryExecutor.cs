using Common.Basic.Blocks;
using Common.Basic.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Basic.CQRS.Query
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly ITypeMapper _typeMapper;
        private readonly Dictionary<string, Type> _dictionary = new Dictionary<string, Type>();

        public QueryExecutor(ITypeMapper typeMapper)
        {
            _typeMapper = typeMapper;
        }

        public Task<Result<TQueryOutput>> Execute<TQuery, TQueryOutput>(TQuery query)
            where TQuery : IQuery
            where TQueryOutput : IQueryOutput
        {
            throw new NotImplementedException();
        }

        public Task<Result<TQueryOutput>> Execute<TQuery, TQueryOutput>()
            where TQuery : IQuery, new()
            where TQueryOutput : IQueryOutput
        {
            throw new NotImplementedException();
        }

        public void RegisterQueryHandler<TQuery, TQueryOutput, TQueryHandler>()
            where TQuery : IQuery
            where TQueryOutput : IQueryOutput
            where TQueryHandler : IQueryHandler<TQuery, TQueryOutput>
        {
            _dictionary.Add(typeof(TQuery).Name, typeof(TQueryHandler));
        }

        Task<Result<TQueryOutput>> IQueryExecutor.Execute<TQuery, TQueryOutput>(TQuery query)
        {
            Type type = GetQueryHandlerType<TQuery>();
            if (type == null)
                return Result<TQueryOutput>.FailureTask();

            var constructors = type.GetConstructors();
            var parametersArrays = constructors.Select(c => c.GetParameters()).ToArray();
            var parameters = parametersArrays.SelectMany(p => p).ToArray();
            var parametersMapped = parameters.Select(p => _typeMapper.Get(p.ParameterType)).ToArray();

            var queryHandler = Activator.CreateInstance(type, parametersMapped) as IQueryHandler<TQuery, TQueryOutput>;
            return queryHandler.Handle(query);
        }

        Task<Result<TQueryOutput>> IQueryExecutor.Execute<TQuery, TQueryOutput>()
        {
            return (this as IQueryExecutor).Execute<TQuery, TQueryOutput>(new TQuery());
        }

        private Type GetQueryHandlerType<TCommand>()
        {
            _dictionary.TryGetValue(typeof(TCommand).Name, out var value);
            return value;
        }
    }
}
