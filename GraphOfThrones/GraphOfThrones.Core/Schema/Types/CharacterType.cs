using System;
using GraphOfThrones.Core.Models;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Types
{
    public class CharacterType : ObjectGraphType<Character>
    {
        public CharacterType()
        {
            Field(c => c.characterName);
            Field(c => c.parents);
            Field(c => c.siblings);
            Field(c => c.characterImageFull);
            Field(c => c.characterImageThumb);
            Field(c => c.killed);
            Field(c => c.killedBy);
        }
    }
}
