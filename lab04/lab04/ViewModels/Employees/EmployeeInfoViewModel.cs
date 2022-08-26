using Entities;
using lab04.Command;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04.ViewModels.Employees
{
    public class EmployeeInfoViewModel : ViewModelBase
    {
        private RepositoryManager _repository;
        public EmployeeInfoViewModel(Employee employee)
        {
            _repository = new RepositoryManager(new RepositoryContext());
            Positions = new List<Position>(_repository.PosititonRepository.GetAllPositions(false));
            UpdateEmployee = new CommandBase(OnUpdate);
            DeleteEmployee = new CommandBase(OnDelete);
            SelectedEmployee = employee;
            SelectedEmployee.Position = Positions.Where(p => p.Id == employee.PositionId).FirstOrDefault();
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

        public CommandBase DeleteEmployee { get; set; }
        public CommandBase UpdateEmployee { get; set; }

        private async void OnUpdate(object obj)
        {
            _repository.EmployeeRepository.Update(SelectedEmployee);
            await _repository.SaveAsync();
        }

        private async void OnDelete(object obj)
        {
            _repository.EmployeeRepository.DeleteEmployee(SelectedEmployee);
            await _repository.SaveAsync();
        }
    }
}
