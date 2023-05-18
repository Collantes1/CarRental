using CarRental.Classes.Entity;
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

namespace CarRental.Forms.PageFrame
{
    /// <summary>
    /// Логика взаимодействия для ListCar.xaml
    /// </summary>
    public partial class ListCar : Page
    {
        public ListCar()
        {
            InitializeComponent();
            //var CarList = CarRentalEntities.GetContext().CarInfo.ToList();
            //LViewCars.ItemsSource = CarList;
        }

        private void ButtonUpdateClientGrid_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEditClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchClient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
