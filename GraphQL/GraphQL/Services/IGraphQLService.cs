using System;
using System.Threading.Tasks;
using GraphQL.Responses;

namespace GraphQL
{
    public interface IGraphQLService
    {
        Task<T> Query<T>(string endpointUrl, string q);
    }
#pragma mark newstuf
    /*public async Task Subscribe()
    {
        var subscriptionResult = await _graphQLClient.SendSubscribeAsync(@"subscription { orderEvent(statuses: [CREATED]) {   name   id   timestamp  }}");
        subscriptionResult.OnReceive += MessageReceived;
    }

    private void MessageReceived(GraphQLResponse  response)
    {
        Console.WriteLine(response.Data.messageAdded.content);
    }*/
#pragma mark 
}
