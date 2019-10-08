using System;
using GraphOfThrones.Core.Schema.Types;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Queries
{
    public class CharactersQuery : ObjectGraphType<object>
    {
        public CharactersQuery()
        {
            Name = "Query";
            Field<ListGraphType<CharacterType>>("characters", resolve: (context) =>
            {
                // TODO:
                return new ListGraphType<CharacterType>();
            });
        }
    }
}
