using DarkHelpers;
using DarkHelpers.Commands;
using Sample.Lib.Models;
using System;
using System.Linq;
using System.Windows.Input;

namespace Sample.Lib.ViewModels
{
    public class ObservableCollectionViewModel : DarkViewModel
    {
        public DarkObservableCollection<Item> Items { get; set; } = new DarkObservableCollection<Item>();

        public ICommand ClearCommand => new DarkCommand(Clear);
        public ICommand AddItemCommand => new DarkCommand(AddItem);
        public ICommand AddItemsCommand => new DarkCommand(AddItems);
        public ICommand ReplaceWithItemCommand => new DarkCommand(ReplaceItem);
        public ICommand ReplaceWithItemsCommand => new DarkCommand(ReplaceItems);

        private void Clear()
        {
            IsBusy = true;
            Items.Clear();
            IsBusy = false;
        }

        private void AddItem()
        {
            IsBusy = true;
            Items.Add(new Item(Guid.NewGuid().ToString()));
            IsBusy = false;
        }

        private void AddItems()
        {
            IsBusy = true;
            Items.AddRange(Enumerable.Range(1,5).Select(a => GetRandomItem()));
            IsBusy = false;
        }

        private void ReplaceItem()
        {
            IsBusy = true;
            Items.Replace(GetRandomItem());
            IsBusy = false;
        }

        private void ReplaceItems()
        {
            IsBusy = true;
            Items.ReplaceRange(Enumerable.Range(1, 5).Select(a => GetRandomItem()));
            IsBusy = false;
        }

        private Item GetRandomItem()
        {
            return new Item(Guid.NewGuid().ToString());
        }
    }
}
