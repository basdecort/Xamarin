using GraphQL.Types;
using Shared.Core.Models;

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
