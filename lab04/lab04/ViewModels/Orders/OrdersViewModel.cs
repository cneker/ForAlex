using Entities;
using lab04.Command;
using lab04.Views.Orders;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace lab04.ViewModels.Orders
{
    public class OrdersViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public OrdersViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            SelectedOrder = new Order();
            MakeOrder = new CommandBase(OnAdd);
            UpdateOrder = new CommandBase(OnUpdate);
            FilterOrdersByName = new CommandBase(OnFilteringByName);
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

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new ObservableCollection<Order>(_repository.OrderRepository.GetAllOrders(false));
                return _orders;
            }
            set
            {
                _orders = value;
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
        public CommandBase MakeOrder { get; set; }
        public CommandBase FilterOrdersByName { get; set; }
        public CommandBase UpdateOrder { get; set; }
        private void OnAdd(object obj)
        {
            var window = new MakeOrder();
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Orders = new ObservableCollection<Order>(_repository.OrderRepository.GetAllOrders(false));
            }
        }
        private void OnUpdate(object obj)
        {
            var window = new OrderInfo(SelectedOrder);
            if(window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Orders = new ObservableCollection<Order>(_repository.OrderRepository.GetAllOrders(false));
            }
        }

        public void OnFilteringByName(object obj)
        {
            Orders = new ObservableCollection<Order>(
                _repository
                .OrderRepository
                .GetAllOrders(false)
                .Where(o => Regex.IsMatch(o.Item.Name.ToLower(), @$"\w*{FilterName.ToLower()}\w*")));
        }
    }
}
