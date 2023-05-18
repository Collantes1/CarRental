using CarRental.Classes.Entity;
using CarRental.Forms.WindowMessage;
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
    /// Логика взаимодействия для PenaltiesPage.xaml
    /// </summary>
    public partial class PenaltiesPage : Page
    {
        int mode;
        int ActionPenaltie;
        string searchPenaltie;
        int ActionClient = 0;
        public PenaltiesPage()
        {
            InitializeComponent();
            var PenaltiesInfo = CarRentalEntities.GetContext().ClientPenalties.ToList();
            PenaltiesGrid.ItemsSource = PenaltiesInfo;
        }

        private void ButtonUpdatePenaltiesGrid_Click(object sender, RoutedEventArgs e)
        {
            var PenaltiesInfo = CarRentalEntities.GetContext().ClientPenalties.ToList();
            PenaltiesGrid.ItemsSource = PenaltiesInfo;
        }

        private void ButtonAddPenalties_Click(object sender, RoutedEventArgs e)
        {
            ClientPenaltieInfo a = new ClientPenaltieInfo(ActionPenaltie = 0, ActionClient);
            a.ShowDialog();
        }

        private void ButtonEditPenalties_Click(object sender, RoutedEventArgs e)
        {
            ClientPenalties cf = (ClientPenalties)PenaltiesGrid.SelectedItem;
            if ((ClientPenalties)PenaltiesGrid.SelectedItem == null)
            {
                ErrorWindows er = new ErrorWindows(mode = 2);
                er.ShowDialog();
            }
            else
            {
                ClientPenaltieInfo a = new ClientPenaltieInfo(ActionPenaltie = cf.CPenaltiesID, ActionClient);
                a.ShowDialog();
            }
        }

        private void SearchPenalties_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchPenaltie = SearchPenalties.Text;
            if (!String.IsNullOrEmpty(searchPenaltie))
            {
                PenaltiesGrid.ItemsSource = ConnectDB.DB.ClientPenalties.Where(x => x.Client.ClientSurname.Contains(searchPenaltie) | x.Client.ClientName.Contains(searchPenaltie) | x.Client.ClientPatronymic.Contains(searchPenaltie)).ToList();
            }
            else
            {
                PenaltiesGrid.ItemsSource = ConnectDB.DB.ClientPenalties.ToList();
            }
        }
    }
}
