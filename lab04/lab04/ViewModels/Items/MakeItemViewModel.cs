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
    public class MakeItemViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public MakeItemViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            AddItem = new CommandBase(OnAdd);
            SelectedItem = new Item();
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

        public CommandBase AddItem { get; set; }

        private async void OnAdd(object obj)
        {
            _repository.ItemRepository.CreateItem(SelectedItem);
            await _repository.SaveAsync();
        }
    }
}
