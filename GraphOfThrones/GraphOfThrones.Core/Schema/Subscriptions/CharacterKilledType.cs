using System;
using GraphQL.Types;
using Shared.Core.Models;

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
