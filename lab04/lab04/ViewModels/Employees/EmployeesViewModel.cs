using Entities;
using lab04.Command;
using lab04.Views.Employees;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab04.ViewModels.Employees
{
    public class EmployeesViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public EmployeesViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            Employees = new ObservableCollection<Employee>(_repository.EmployeeRepository.GetAllEmployees(false));
            MakeEmployee = new CommandBase(OnAdd);
            UpdateEmployee = new CommandBase(OnUpdate);
            FilterEmployeesByAge = new CommandBase(OnFilteringByAge);
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

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }
        private int _filterAge;
        public int FilterAge
        {
            get => _filterAge;
            set
            {
                _filterAge = value;
                OnPropertyChanged();
            }
        }
        public CommandBase FilterEmployeesByAge { get; set; }
        public CommandBase MakeEmployee { get; set; }
        public CommandBase UpdateEmployee { get; set; }

        private async void OnAdd(object obj)
        {
            var window = new MakeEmployee();
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Employees = new ObservableCollection<Employee>(_repository.EmployeeRepository.GetAllEmployees(false));
            }
        }

        private async void OnUpdate(object obj)
        {
            var window = new EmployeeInfo(SelectedEmployee);
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Employees = new ObservableCollection<Employee>(_repository.EmployeeRepository.GetAllEmployees(false));
            }
        }

        public void OnFilteringByAge(object obj)
        {
            if (FilterAge != 0)
                Employees = new ObservableCollection<Employee>(
                    _repository
                    .EmployeeRepository
                    .GetAllEmployees(false)
                    .Where(i => i.Age == FilterAge));
            else
                Employees = new ObservableCollection<Employee>(
                                    _repository
                                    .EmployeeRepository
                                    .GetAllEmployees(false));
        }
    }
}
