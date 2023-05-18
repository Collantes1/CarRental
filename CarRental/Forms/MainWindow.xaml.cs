using CarRental.Classes.Entity;
using CarRental.Forms.PageFrame;
using FontAwesome.Sharp;
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

namespace CarRental.Forms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImageClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ImageHide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectDB.DB = new Classes.Entity.CarRentalEntities();
            TabFrame.NavigationService.Navigate(new ListCar());
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RadioButtonCar_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Автомобили";
            IconCaption.Icon = IconChar.List;
            TabFrame.NavigationService.Navigate(new ListCar());
        }

        private void RadioButtonBooking_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Бронирование";
            IconCaption.Icon = IconChar.Clock;
            //TabFrame.NavigationService.Navigate(new ClientPage());
        }

        private void RadioButtonRent_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Аренда";
            IconCaption.Icon = IconChar.CarSide;
            //TabFrame.NavigationService.Navigate(new ClientPage());
        }

        private void RadioButtonClient_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Клиенты";
            IconCaption.Icon = IconChar.Person;
            TabFrame.NavigationService.Navigate(new ClientPage());
        }

        private void RadioButtonTicket_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Штрафы";
            IconCaption.Icon = IconChar.MoneyCheck;
            TabFrame.NavigationService.Navigate(new PenaltiesPage());
        }

        private void RadioButtonEmail_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Email";
            IconCaption.Icon = IconChar.MailBulk;
            TabFrame.NavigationService.Navigate(new EmailPage());
        }

        private void RadioButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Администрирование";
            IconCaption.Icon = IconChar.Lock;
            //TabFrame.NavigationService.Navigate(new ClientPage());
        }

        private void UserSettings_Click(object sender, RoutedEventArgs e)
        {
            TextCaption.Text = "Настройки пользователя";
            IconCaption.Icon = IconChar.Gear;
            //TabFrame.NavigationService.Navigate(new ClientPage());
        }
    }
}
