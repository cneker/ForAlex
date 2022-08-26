using Entities;
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
    /// Логика взаимодействия для OrganizationInfo.xaml
    /// </summary>
    public partial class OrganizationInfo : Window
    {
        public OrganizationInfo(Organization organization)
        {
            InitializeComponent();
            DataContext = new OrganizationInfoViewModel(organization);
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
