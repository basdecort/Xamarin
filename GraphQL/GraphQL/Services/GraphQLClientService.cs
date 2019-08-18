using System;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Models;
using GraphQL.Responses;

namespace GraphQL.Services
{
    public class GraphQLClientService : IGraphQLService
    {
        private GraphQLClient _graphQLClient;

        public GraphQLClientService()
        {
      
        }

        public async Task<T> Query<T>(string endpointUrl, string q)
        {
            if (_graphQLClient == null)
            {
                _graphQLClient = new GraphQLClient(endpointUrl);
                _graphQLClient.DefaultRequestHeaders.Add("User-Agent", "GraphQL");
                _graphQLClient.DefaultRequestHeaders.Add("Authorization", "bearer ");
            }

            var response = await _graphQLClient.PostQueryAsync(q);
            
            // dynamic
            // var value = response.Data.name;

            // Typed
            var user = response.GetDataFieldAs<T>("user");

            return user;
        }
    }
}
