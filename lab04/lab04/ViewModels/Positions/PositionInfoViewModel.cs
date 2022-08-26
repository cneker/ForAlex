using Entities;
using lab04.Command;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04.ViewModels.Positions
{
    public class PositionInfoViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public PositionInfoViewModel(Position position)
        {
            _repository = new RepositoryManager(new RepositoryContext());
            DeletePosition = new CommandBase(OnDelete);
            UpdatePosition = new CommandBase(OnUpdate);
            SelectedPosition = position;
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

        public CommandBase UpdatePosition { get; set; }
        public CommandBase DeletePosition { get; set; }

        private async void OnDelete(object obj)
        {
            _repository.PosititonRepository.DeletePosition(SelectedPosition);
            await _repository.SaveAsync();
        }

        private async void OnUpdate(object obj)
        {
            _repository.PosititonRepository.Update(SelectedPosition);
            await _repository.SaveAsync();
        }
    }
}
