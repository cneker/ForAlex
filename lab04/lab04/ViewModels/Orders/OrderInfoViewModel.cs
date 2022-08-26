using Entities;
using lab04.Command;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04.ViewModels.Orders
{
    public class OrderInfoViewModel : ViewModelBase
    {
        private RepositoryManager _repository;
        public RepositoryManager Repository
        {
            get => _repository;
            set
            {
                _repository = value;
                OnPropertyChanged();
            }
        }

        public OrderInfoViewModel(Order order)
        {
            Repository = new RepositoryManager(new RepositoryContext());
            UpdateOrder = new CommandBase(OnUpdate);
            DeleteOrder = new CommandBase(OnDelete);

            Employees = Repository.EmployeeRepository.GetAllEmployees(false).ToList();
            Organizations = Repository.OrganizationRepository.GetAllOrganizations(false).ToList();
            Items = Repository.ItemRepository.GetAllItems(false).ToList();
            SelectedOrder = order;
            SelectedOrder.Employee = Employees.FirstOrDefault(e => e.Id == order.Employee.Id);
            SelectedOrder.Organization = Organizations.FirstOrDefault(e => e.Id == order.Organization.Id);
            SelectedOrder.Item = Items.FirstOrDefault(e => e.Id == order.Item.Id);
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }
        public List<Employee> Employees { get; set; }
        public List<Item> Items { get; set; }
        public List<Organization> Organizations {get; set;}

        public CommandBase UpdateOrder { get; set; }
        public CommandBase DeleteOrder { get; set; }

        private async void OnUpdate(object obj)
        {
            _repository.OrderRepository.Update(SelectedOrder);
            await _repository.SaveAsync();
        }
        private async void OnDelete(object obj)
        {
            _repository.OrderRepository.DeleteOrder(SelectedOrder);
            await _repository.SaveAsync();
        }
    }
}

