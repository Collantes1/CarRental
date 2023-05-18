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
    /// Логика взаимодействия для EmailPage.xaml
    /// </summary>
    public partial class EmailPage : Page
    {
        int ActionClient;
        string searchmail;
        public EmailPage()
        {
            InitializeComponent();
            var MessagesInfo = CarRentalEntities.GetContext().Messages.ToList();
            MessagesGrid.ItemsSource = MessagesInfo;
        }

        private void ButtonUpdateEmailGrid_Click(object sender, RoutedEventArgs e)
        {
            var MessagesInfo = CarRentalEntities.GetContext().Messages.ToList();
            MessagesGrid.ItemsSource = MessagesInfo;
        }

        private void ButtonAddEmail_Click(object sender, RoutedEventArgs e)
        {
            ClientMessageInfo a = new ClientMessageInfo(ActionClient = 0);
            a.ShowDialog();
        }

        private void SearchMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchmail = SearchMail.Text;
            if (!String.IsNullOrEmpty(searchmail))
            {
                MessagesGrid.ItemsSource = ConnectDB.DB.Messages.Where(x => x.Client.ClientSurname.Contains(searchmail) | x.Client.ClientName.Contains(searchmail) | x.Client.ClientPatronymic.Contains(searchmail)).ToList();
            }
            else
            {
                MessagesGrid.ItemsSource = ConnectDB.DB.Messages.ToList();
            }
        }
    }
}
