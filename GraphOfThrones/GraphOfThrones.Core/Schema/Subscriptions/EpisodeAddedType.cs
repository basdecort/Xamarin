using System;
using GraphOfThrones.Core.Models;
using GraphOfThrones.Core.Schema.Types;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Subscriptions
{
    public class EpisodeAddedType : ObjectGraphType<Episode>
    {
        public EpisodeAddedType()
        {
            Field("Title", e => e.episodeTitle);
            Field<DateTime>("AirDate", e => DateTime.Now);
        }
    }
}
