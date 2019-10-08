using System;
using GraphOfThrones.Core.Models;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Types
{
    public class CharacterType : ObjectGraphType<Character>
    {
        public CharacterType()
        {
            Field(c => c.name);
            Field(c => c.gender);
        }
    }
}
