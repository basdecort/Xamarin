using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOfThrones.Core.Models;

namespace GOTKilled.Services
{
    public class CharacterService : ICharacterService
    {
        public CharacterService()
        {
            //http://graphofthrones.azurewebsites.net/
        }

        public Task<List<Character>> GetAll()
        {
           
        }
    }
}
