using Entities;
using lab04.Command;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04.ViewModels.Positions
{
    public class MakePositionViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public MakePositionViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            AddPosition = new CommandBase(OnAdd);
            SelectedPosition = new Position();
        }

        private Position _selectedPosition;
        public Position SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                _selectedPosition = value;
                OnPropertyChanged();
            }
        }

        public CommandBase AddPosition { get; set; }

        private async void OnAdd(object obj)
        {
            _repository.PosititonRepository.CreatePosition(SelectedPosition);
            await _repository.SaveAsync();
        }
    }
}
