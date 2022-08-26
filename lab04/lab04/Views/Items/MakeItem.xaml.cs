using lab04.ViewModels.Items;
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

namespace lab04.Views.Items
{
    /// <summary>
    /// Логика взаимодействия для MakeItem.xaml
    /// </summary>
    public partial class MakeItem : Window
    {
        public MakeItem()
        {
            InitializeComponent();
            DataContext = new MakeItemViewModel();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
