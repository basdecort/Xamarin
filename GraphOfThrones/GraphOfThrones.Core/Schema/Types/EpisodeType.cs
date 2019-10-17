using System;
using GraphOfThrones.Core.Models;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Types
{
    public class EpisodeType : ObjectGraphType<Episode>
    {
        public EpisodeType()
        {
            Field("Title", e => e.episodeTitle); // Field with a custom name
            // TODO: Field("Trailer", "")
        }
    }
}
