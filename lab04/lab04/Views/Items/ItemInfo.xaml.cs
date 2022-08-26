using Entities;
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
    /// Логика взаимодействия для ItemInfo.xaml
    /// </summary>
    public partial class ItemInfo : Window
    {
        public ItemInfo(Item item)
        {
            InitializeComponent();
            DataContext = new ItemInfoViewModel(item);
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
