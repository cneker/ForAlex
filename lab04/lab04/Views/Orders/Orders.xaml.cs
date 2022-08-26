using lab04.ViewModels;
using lab04.ViewModels.Orders;
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
using System.Windows.Shapes;

namespace lab04.Views.Orders
{
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
            DataContext = new OrdersViewModel();
        }
    }
}
