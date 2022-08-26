using lab04.ViewModels.Employees;
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

namespace lab04.Views.Employees
{
    /// <summary>
    /// Логика взаимодействия для MakeEmployee.xaml
    /// </summary>
    public partial class MakeEmployee : Window
    {
        public MakeEmployee()
        {
            InitializeComponent();
            DataContext = new MakeEmployeeViewModel();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
