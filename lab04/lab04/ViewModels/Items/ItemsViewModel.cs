using Entities;
using lab04.Command;
using lab04.Views.Items;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace lab04.ViewModels.Items
{
    public class ItemsViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public ItemsViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            Items = new ObservableCollection<Item>(_repository.ItemRepository.GetAllItems(false));
            MakeItem = new CommandBase(OnAdd);
            UpdateItem = new CommandBase(OnUpdate);
            FilterItemsByName = new CommandBase(OnFilteringByName);
        }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        private string _filterName;
        public string FilterName
        {
            get => _filterName;
            set
            {
                _filterName = value;
                OnPropertyChanged();
            }
        }
        public CommandBase UpdateItem { get; set; }
        public CommandBase FilterItemsByName { get; set; }
        public CommandBase MakeItem { get; set; }

        private async void OnAdd(object obj)
        {
            var window = new MakeItem();
            if(window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Items = new ObservableCollection<Item>(_repository.ItemRepository.GetAllItems(false));
            }
        }

        private async void OnUpdate(object obj)
        {
            var window = new ItemInfo(SelectedItem);
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Items = new ObservableCollection<Item>(_repository.ItemRepository.GetAllItems(false));
            }
        }

        public void OnFilteringByName(object obj)
        {
            Items = new ObservableCollection<Item>(
                _repository
                .ItemRepository
                .GetAllItems(false)
                .Where(i => Regex.IsMatch(i.Name.ToLower(), @$"\w*{FilterName.ToLower()}\w*")));
        }
    }
}
