using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GOTKilled.Models;
using GOTKilled.Views;
using GOTKilled.Services;
using Shared.Core.Models;

namespace GOTKilled.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Character> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        private ICharacterService characterService;

        public ItemsViewModel()
        {
            characterService = new CharacterService();
            Title = "Who GOT killed?";
            Items = new ObservableCollection<Character>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await characterService.GetAll();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}