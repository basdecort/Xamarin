using System;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Mutations
{
    public class KillCharacterRequest : InputObjectGraphType
    {
        public string characterName { get; set; }
        public string killedBy { get; set; }

        public KillCharacterRequest()
        {
            Name = "KillCharacterRequest";
            Field<NonNullGraphType<StringGraphType>>("characterName");
            Field<NonNullGraphType<StringGraphType>>("killedBy");
        }
    }
}
