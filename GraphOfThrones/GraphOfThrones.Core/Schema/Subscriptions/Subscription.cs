using GraphOfThrones.Core.Schema.Types;
using GraphOfThrones.Core.Services;
using GraphQL.Resolvers;
using GraphQL.Types;
using Shared.Core.Models;

namespace GraphOfThrones.Core.Schema.Subscriptions
{
    public class Subscription : ObjectGraphType<object>
    {
        public Subscription(IEpisodeService episodeService, ICharacterService characterService)
        {
            Name = "Subscription";

            AddField(new EventStreamFieldType
            {
                Name = "episodeAdded",
                Type = typeof(EpisodeType),
                Resolver = new FuncFieldResolver<Episode>((context) => context.Source as Episode),
                Subscriber = new EventStreamResolver<Episode>((context) => episodeService.EpisodeAdded())
            });

            AddField(new EventStreamFieldType
            {
                Name = "characterKilled",
                Type = typeof(CharacterKilledType),
                Resolver = new FuncFieldResolver<Character>((context) => context.Source as Character),
                Subscriber = new EventStreamResolver<Character>((context) => characterService.CharacterKilled())
            });
        }

    }
}
