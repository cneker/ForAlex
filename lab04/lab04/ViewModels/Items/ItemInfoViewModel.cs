using Entities;
using lab04.Command;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04.ViewModels.Items
{
    public class ItemInfoViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public ItemInfoViewModel(Item item)
        {
            _repository = new RepositoryManager(new RepositoryContext());
            UpdateItem = new CommandBase(OnUpdate);
            DeleteItem = new CommandBase(OnDelete);
            SelectedItem = item;
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

        public CommandBase DeleteItem { get; set; }
        public CommandBase UpdateItem { get; set; }

        private async void OnUpdate(object obj)
        {
            _repository.ItemRepository.Update(SelectedItem);
            await _repository.SaveAsync();
        }

        private async void OnDelete(object obj)
        {
            _repository.ItemRepository.DeleteItem(SelectedItem);
            await _repository.SaveAsync();
        }
    }
}
