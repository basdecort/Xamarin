using System;
using GraphOfThrones.Core.Models;
using GraphOfThrones.Core.Schema.Types;
using GraphOfThrones.Core.Services;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Mutations
{
    public class Mutation : ObjectGraphType<object>
    {
        private const string _episodeArgumentName = "episode";
        public Mutation(IEpisodeService episodeService)
        {
            Name = "Mutation";

            Field<EpisodeType>(
                 "addEpisode", // name of this mutation
                 arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AddEpisodeRequest>> { Name = _episodeArgumentName }), // Arguments
                 resolve: context =>
                 {
                     // Get Argument 
                     var addEpisodeRequest = context.GetArgument<AddEpisodeRequest>(_episodeArgumentName);
                     // Create new Episode and return the object
                     var episode = new Episode { episodeTitle = addEpisodeRequest.Name, episodeDescription = addEpisodeRequest.Description };
                     return episodeService.Create(episode);
                 }
             );
        }
    }
}
