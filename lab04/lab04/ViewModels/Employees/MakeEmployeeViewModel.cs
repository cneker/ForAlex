using Entities;
using lab04.Command;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04.ViewModels.Employees
{
    public class MakeEmployeeViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public MakeEmployeeViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            Positions = new List<Position>(_repository.PosititonRepository.GetAllPositions(false));
            AddEmployee = new CommandBase(OnAdd);
            SelectedEmployee = new Employee() { Position = new Position() };
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public List<Position> Positions { get; set; }

        public CommandBase AddEmployee { get; set; }

        private async void OnAdd(object obj)
        {
            SelectedEmployee.PositionId = SelectedEmployee.Position.Id;
            SelectedEmployee.Position = null;
            _repository.EmployeeRepository.CreateEmployee(SelectedEmployee);
            await _repository.SaveAsync();
        }
    }
}
