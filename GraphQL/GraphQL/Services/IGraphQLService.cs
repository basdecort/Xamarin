using System;
using System.Threading.Tasks;
using GraphQL.Responses;

namespace GraphQL
{
    public interface IGraphQLService
    {
        Task<T> Query<T>(string endpointUrl, string q);
    }
}
