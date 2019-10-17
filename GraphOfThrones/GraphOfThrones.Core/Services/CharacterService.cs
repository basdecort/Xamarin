using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOfThrones.Core.Models;

namespace GraphOfThrones.Core.Services
{
    public interface ICharacterService
    {
        Task<List<Models.Character>> GetAll();
    }

    public class CharacterService : ICharacterService
    {
        public CharacterService()
        {
        }

        public Task<List<Character>> GetAll()
        {
            var characters = new List<Character>
            {
                new Character()
                {
                    name = "Name",
                    gender = "Male"
                }
            };
            return Task.FromResult(characters);
        }
    }
}
