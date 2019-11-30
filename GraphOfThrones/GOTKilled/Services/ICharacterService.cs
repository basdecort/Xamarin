using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOfThrones.Core.Models;

namespace GOTKilled.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAll();   
    }
}
