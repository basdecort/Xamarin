using System;
using GraphOfThrones.Core.Models;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Subscriptions
{
    public class CharacterKilledType : ObjectGraphType<Character>
    {
        public CharacterKilledType()
        {
            Field(c => c.characterName);
            Field(c => c.killedBy);
        }
    }
}
