using System;
using GraphOfThrones.Core.Models;
using GraphOfThrones.Core.Schema.Types;
using GraphOfThrones.Core.Services;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Queries
{
    public class CharactersQuery : ObjectGraphType<object>
    {
        public CharactersQuery(IService<Character> characterService, IService<Episode> episodeService)
        {
            Name = "Query";
            Field<ListGraphType<CharacterType>>("characters", resolve: (context) => characterService.GetAll());
            Field<ListGraphType<CharacterType>>("episodes", resolve: (context) => episodeService.GetAll());
        }
    }
}
