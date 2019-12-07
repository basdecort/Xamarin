using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphOfThrones.Core.Services;
using GraphQL.Types;
using Shared.Core.Models;

namespace GraphOfThrones.Core.Schema.Types
{
    public class CharacterType : ObjectGraphType<Character>
    {
        public CharacterType(IEpisodeService episodeService)
        {
            Field(c => c.characterName);
            Field(c => c.parents);
            Field(c => c.siblings);
            Field(c => c.characterImageFull);
            Field(c => c.characterImageThumb);
            Field(c => c.killed);
            Field(c => c.killedBy);
            Field<ListGraphType<EpisodeType>>("episodes", resolve: (context) => GetAllEpisodesForCharacter(episodeService, context.Source.characterName));
        }

        private static async Task<IEnumerable<Episode>> GetAllEpisodesForCharacter(IEpisodeService episodeService, string characterName)
        {
            var allEpisodes = await episodeService.GetAll();
            return allEpisodes.Where(e => e.scenes.SelectMany(s => s.characters).Any(c => c.name == characterName));
        }
    }
}
