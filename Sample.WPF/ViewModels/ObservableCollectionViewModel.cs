using DarkHelpers;
using DarkHelpers.Commands;
using Sample.WPF.Models;
using System;
using System.Linq;
using System.Windows.Input;

namespace Sample.WPF.ViewModels
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
            Items.Clear();
        }

        private void AddItem()
        {
            Items.Add(new Item(Guid.NewGuid().ToString()));
        }

        private void AddItems()
        {
            Items.AddRange(Enumerable.Range(1,5).Select(a => GetRandomItem()));
        }

        private void ReplaceItem()
        {
            Items.Replace(GetRandomItem());
        }

        private void ReplaceItems()
        {
            Items.ReplaceRange(Enumerable.Range(1, 5).Select(a => GetRandomItem()));
        }

        private Item GetRandomItem()
        {
            return new Item(Guid.NewGuid().ToString());
        }
    }
}
