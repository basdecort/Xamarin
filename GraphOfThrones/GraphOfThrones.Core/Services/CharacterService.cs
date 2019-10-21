using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphOfThrones.Core.Models;
using Newtonsoft.Json;

namespace GraphOfThrones.Core.Services
{
    public class CharacterResult
    {
        public List<Character> characters { get; set; }
    }

    public interface ICharacterService : IService<Character>
    {
        Task<Character> Kill(string characterName, string killedBy);
    }

    public class CharacterService : ServiceBase<CharacterResult>, ICharacterService
    {
        public CharacterService() : base("https://raw.githubusercontent.com/jeffreylancaster/game-of-thrones/master/data/characters.json")
        {}

        public async Task<IEnumerable<Character>> GetAll()
        {
            var result = await Get();

            return result.characters;
        }

        public async Task<Character> Kill(string characterName, string killedBy)
        {
            if (CachedResult?.characters == null)
            {
                await GetAll();
            }

            var character = CachedResult.characters.FirstOrDefault(c => c.characterName.ToLower() == characterName.ToLower());
            if (character == null)
            {
                throw new ArgumentOutOfRangeException("Character with name: "+characterName+" not found");
            }

            if (character.killedBy != null && character.killedBy.Any(c => !string.IsNullOrEmpty(c)))
            {
                throw new ArgumentOutOfRangeException("Character with name: " + characterName + " was already killed");
            }

            character.killedBy = new List<string>
            {
                killedBy
            };

            return character;
        }
    }
}
