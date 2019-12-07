using System;

using GOTKilled.Models;
using Shared.Core.Models;

namespace GOTKilled.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Character Item { get; set; }
        public ItemDetailViewModel(Character item = null)
        {
            Title = item?.characterName;
            Item = item;
        }
    }
}
