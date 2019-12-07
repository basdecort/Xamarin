using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Core.Models;

namespace GOTKilled.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAll();
        Task<Character> GetDetails(string characterName);
    }
}
