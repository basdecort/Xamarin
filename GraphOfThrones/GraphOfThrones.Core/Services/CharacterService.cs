using System;
using System.Collections.Generic;
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
    
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAll();
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
    }
}
