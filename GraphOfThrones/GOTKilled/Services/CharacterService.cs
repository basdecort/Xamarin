using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Shared.Core.Models;

namespace GOTKilled.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly GraphQLClient graphQLClient;
        public CharacterService()
        {
            //http://graphofthrones.azurewebsites.net/
            graphQLClient = new GraphQLClient("http://graphofthrones.azurewebsites.net/graphql");
        }

        /// <summary>
        /// This will fetch all information required to show a list of characters
        /// </summary>
        /// <returns></returns>
        public async Task<List<Character>> GetAll()
        {
            var request = new GraphQLRequest
            {
               Query = @"{
                          characters
                          {
                            characterName,
                            characterImageThumb,
                            nickname
                          }
                        }"
            };

            var response = await graphQLClient.PostAsync(request);
            return response.GetDataFieldAs<List<Character>>("characters");
        }

        /// <summary>
        /// This will fetch the details that are shown on the detail page.
        /// </summary>
        /// <returns></returns>
        public async Task<Character> GetDetails(string characterName)
        {
            var request = new GraphQLRequest
            {
                Query = @"{
                          characters
                          {
                            characterName,
                            characterImageThumb,
                            characterImageFull,
                            nickname,
                            killed,
                            killedBy
                          }
                        }"
            };

            var response = await graphQLClient.PostAsync(request);
            // TODO: we should enable the API to allow filtering 
            return response.GetDataFieldAs<List<Character>>("characters").FirstOrDefault(c => c.characterName == characterName);
        }
    }
}
