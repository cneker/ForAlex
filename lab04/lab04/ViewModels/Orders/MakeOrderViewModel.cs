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
    public class MakeOrderViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public MakeOrderViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            MakeOrder = new CommandBase(OnMake);
            Employees = _repository.EmployeeRepository.GetAllEmployees(false).ToList();
            Organizations = _repository.OrganizationRepository.GetAllOrganizations(false).ToList();
            Items = _repository.ItemRepository.GetAllItems(false).ToList();
            SelectedOrder = new Order();
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
        public List<Organization> Organizations { get; set; }

        public CommandBase MakeOrder { get; set; }

        private async void OnMake(object obj)
        {
            var order = new Order();
            order.Amount = SelectedOrder.Amount;
            order.OrganizationId = SelectedOrder.Organization.Id;
            order.ItemId = SelectedOrder.Item.Id;
            order.EmployeeId = SelectedOrder.Employee.Id;
            _repository.OrderRepository.CreateOrder(order);
            await _repository.SaveAsync();
        }
    }
}
