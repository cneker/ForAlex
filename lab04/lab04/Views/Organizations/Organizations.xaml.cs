using lab04.ViewModels;
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
    /// Логика взаимодействия для Organizations.xaml
    /// </summary>
    public partial class Organizations : Window
    {
        public Organizations()
        {
            InitializeComponent();
            DataContext = new OrganizationsViewModel();
        }
    }
}
