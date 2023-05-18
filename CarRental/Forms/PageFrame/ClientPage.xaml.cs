using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarRental.Classes.Entity;
using CarRental.Forms.WindowMessage;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CarRental.Forms.PageFrame
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        int mode;
        int ActionClient = 0;
        string clientsearch;
        public ClientPage()
        {
            InitializeComponent();
            var ClientInfoGrid = CarRentalEntities.GetContext().Client.ToList();
            ClientGrid.ItemsSource = ClientInfoGrid;
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientInfoWindow a = new ClientInfoWindow(ActionClient = 0);
            a.ShowDialog();
        }

        private void ButtonEditClient_Click(object sender, RoutedEventArgs e)
        {
            Client clientinfo = (Client)ClientGrid.SelectedItem;
            if ((Client)ClientGrid.SelectedItem == null)
            {
                ErrorWindows a = new ErrorWindows(mode = 1);
                a.ShowDialog();
            }
            else
            {
                ClientInfoWindow a = new ClientInfoWindow(ActionClient = clientinfo.ClientID);
                a.ShowDialog();
            }
        }

        private void ButtonUpdateClientGrid_Click(object sender, RoutedEventArgs e)
        {
            var ClientInfoGrid = CarRentalEntities.GetContext().Client.ToList();
            ClientGrid.ItemsSource = ClientInfoGrid;
        }

        private void SearchClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            clientsearch = SearchClient.Text;
            if (!String.IsNullOrEmpty(clientsearch))
            {
                ClientGrid.ItemsSource = ConnectDB.DB.Client.Where(x => x.ClientSurname.Contains(clientsearch) | x.ClientName.Contains(clientsearch) | x.ClientPatronymic.Contains(clientsearch)).ToList();
            }
            else
            {
                ClientGrid.ItemsSource = ConnectDB.DB.Client.ToList();
            }
        }
    }
}
