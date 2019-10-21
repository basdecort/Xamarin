using System;
using GraphOfThrones.Core.Models;
using GraphOfThrones.Core.Schema.Types;
using GraphOfThrones.Core.Services;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Queries
{
    public class Query : ObjectGraphType<object>
    {
        public Query(ICharacterService characterService, IEpisodeService episodeService)
        {
            Name = "Query";
            // Expose characters
            Field<ListGraphType<CharacterType>>("characters", resolve: (context) => characterService.GetAll());
            // Expose episodes
            Field<ListGraphType<EpisodeType>>("episodes", resolve: (context) => episodeService.GetAll());
        }
    }
}
