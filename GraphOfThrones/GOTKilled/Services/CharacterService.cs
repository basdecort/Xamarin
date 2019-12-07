using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Core.Models;

namespace GOTKilled.Services
{
    public class CharacterService : ICharacterService
    {
        public CharacterService()
        {
            //http://graphofthrones.azurewebsites.net/
        }

        /// <summary>
        /// This will fetch all information required to show a list of characters
        /// </summary>
        /// <returns></returns>
        public async Task<List<Character>> GetAll()
        {
            return new List<Character>
            {
                new Character
                {
                    characterName = "hi",
                    characterImageThumb = "https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/customizing-cell-appearance-images/text-cell-default.png",
                    nickname = "holo",
                }
            };
        }

        /// <summary>
        /// This will fetch the details that are shown on the detail page.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Character>> GetDetails()
        {
            return null;
        }
    }
}
