using System;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Mutations
{
    public class AddEpisodeRequest : InputObjectGraphType
    {
        public AddEpisodeRequest()
        {
            Name = "EpisodeRequest";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("description");
        }
    }
}
