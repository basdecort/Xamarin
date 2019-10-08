using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.Responses;
using Newtonsoft.Json;

namespace GraphQL
{
    public class GraphQLHttpService : IGraphQLService
    {
        private readonly HttpClient _httpClient;

        public GraphQLHttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "GraphQL");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "bearer 6e12ba408462d0e362cfab249e1cfd7603973dc0");
        }

        public async Task<T> Query<T>(string endpointUrl, string q)
        {
            try
            {
                var query = "{ \"query\": \" " + q + "  \"  }";
                var stringContent = new StringContent(query);

                var response = await _httpClient.PostAsync(endpointUrl, stringContent);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                
                var queryResponse =  JsonConvert.DeserializeObject<QueryResponse<UserData<T>>>(json);

                return queryResponse.data.user;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public Task Subscribe()
        {
            return Task.CompletedTask;
        }
    }
}
