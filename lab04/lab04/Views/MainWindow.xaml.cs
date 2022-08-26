using lab04.Views.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab04.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Employees(Object sender, RoutedEventArgs e)
        {
            var employees = new Employees.Employees();
            employees.ShowDialog();
        }
        private void Items(Object sender, RoutedEventArgs e)
        {
            var items = new Items.Items();
            items.ShowDialog();
        }
        private void Organizations(Object sender, RoutedEventArgs e)
        {
            var organizations = new Organizations.Organizations();
            organizations.ShowDialog();
        }
        private void Positions(Object sender, RoutedEventArgs e)
        {
            var positions = new Positions.Positions();
            positions.ShowDialog();
        }
        private void Orders(Object sender, RoutedEventArgs e)
        {
            var orders = new Orders.Orders();
            orders.ShowDialog();
        }
    }
}
