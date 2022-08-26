using Entities;
using lab04.Command;
using lab04.Views.Positions;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace lab04.ViewModels.Positions
{
    public class PositionsViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public PositionsViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            Positions = new ObservableCollection<Position>(_repository.PosititonRepository.GetAllPositions(false));
            MakePosition = new CommandBase(OnAdd);
            UpdatePosition = new CommandBase(OnUpdate);
            FilterPositionsByName = new CommandBase(OnFilteringByName);
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

        private ObservableCollection<Position> _positions;
        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set
            {
                _positions = value;
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
        public CommandBase UpdatePosition { get; set; }
        public CommandBase FilterPositionsByName { get; set; }
        public CommandBase MakePosition { get; set; }

        private async void OnAdd(object obj)
        {
            var window = new MakePosition();
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Positions = new ObservableCollection<Position>(_repository.PosititonRepository.GetAllPositions(false));
            }
        }

        private async void OnUpdate(object obj)
        {
            var window = new PositionInfo(SelectedPosition);
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Positions = new ObservableCollection<Position>(_repository.PosititonRepository.GetAllPositions(false));
            }
        }

        public void OnFilteringByName(object obj)
        {
            Positions = new ObservableCollection<Position>(
                _repository
                .PosititonRepository
                .GetAllPositions(false)
                .Where(p => Regex.IsMatch(p.Name.ToLower(), @$"\w*{FilterName.ToLower()}\w*")));
        }
    }
}
