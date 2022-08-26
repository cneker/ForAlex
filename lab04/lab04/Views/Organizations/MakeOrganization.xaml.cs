using lab04.ViewModels.Organizations;
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

namespace lab04.Views.Organizations
{
    /// <summary>
    /// Логика взаимодействия для MakeOrganization.xaml
    /// </summary>
    public partial class MakeOrganization : Window
    {
        public MakeOrganization()
        {
            InitializeComponent();
            DataContext = new MakeOrganizationViewModel();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
