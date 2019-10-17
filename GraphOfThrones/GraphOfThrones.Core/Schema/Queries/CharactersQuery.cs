using System;
using GraphOfThrones.Core.Schema.Types;
using GraphOfThrones.Core.Services;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Queries
{
    public class CharactersQuery : ObjectGraphType<object>
    {
        public CharactersQuery(ICharacterService characterService)
        {
            Name = "Query";
            Field<ListGraphType<CharacterType>>("characters", resolve: (context) => characterService.GetAll());
        }
    }
}
