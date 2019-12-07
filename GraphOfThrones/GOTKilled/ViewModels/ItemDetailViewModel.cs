using System;
using System.Threading.Tasks;
using GOTKilled.Models;
using GOTKilled.Services;
using Shared.Core.Models;

namespace GOTKilled.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public bool ShowInfo { get; set; }

        public bool IsAlive => Item?.killedBy == null;
        public bool IsDead => !IsAlive;
        public bool KilledPeople => Item?.killed != null;

        public bool ShowAtLeast => KilledPeople && IsDead;
        public bool ShowAnd => KilledPeople && !IsDead;
   
        public Character Item { get; set; }

        private readonly ICharacterService characterService;

        public ItemDetailViewModel(Character item = null)
        {
            ShowInfo = false;
            characterService = new CharacterService();
            Title = item?.characterName;
            Item = item;
        }

        public async Task GetAdditionalInfo()
        {
            var character = await characterService.GetDetails(Item.characterName);
            if (character != null)
            {
                Item = character;
                OnPropertyChanged("Item");
                OnPropertyChanged("IsAlive");
                OnPropertyChanged("IsDead");
                OnPropertyChanged("KilledPeople");
                OnPropertyChanged("ShowAtLeast");
                OnPropertyChanged("ShowAnd");
            }
            ShowInfo = true;
            OnPropertyChanged("ShowInfo");
        }
    }
}
