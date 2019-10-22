using System;
using GraphOfThrones.Core.Models;
using GraphOfThrones.Core.Services;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Subscriptions
{
    public class Subscription : ObjectGraphType<object>
    {
        public Subscription(IEpisodeService episodeService)
        {
            Name = "Subscription";

            AddField(new EventStreamFieldType
            {
                Name = "episodeAdded",
                Type = typeof(EpisodeAddedType),
                Resolver = new FuncFieldResolver<Episode>((context) => context.Source as Episode),
                Subscriber = new EventStreamResolver<Episode>((context) => episodeService.EpisodeAdded())
            });

            /*AddField(new EventStreamFieldType
            {
                Name = "characterKilled",
                Type = typeof(EpisodeAddedType),
                Resolver = new FuncFieldResolver<Episode>((context) => context.Source as Episode),
                Subscriber = new EventStreamResolver<Episode>((context) => episodeService.EpisodeAdded())
            });*/
        }

    }
}
