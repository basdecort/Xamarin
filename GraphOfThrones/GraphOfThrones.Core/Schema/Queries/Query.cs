using System;
using GraphOfThrones.Core.Models;
using GraphOfThrones.Core.Schema.Types;
using GraphOfThrones.Core.Services;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Queries
{
    public class Query : ObjectGraphType<object>
    {
        public Query(IService<Character> characterService, IService<Episode> episodeService)
        {
            Name = "Query";
            // Expose characters Query
            Field<ListGraphType<CharacterType>>("characters", resolve: (context) => characterService.GetAll());
            // Expose episodes Query
            Field<ListGraphType<EpisodeType>>("episodes", resolve: (context) => episodeService.GetAll());
        }
    }
}
