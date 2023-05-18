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
using System.Windows.Shapes;

namespace CarRental.Forms
{
    /// <summary>
    /// Логика взаимодействия для ClientPenaltieInfo.xaml
    /// </summary>
    public partial class ClientPenaltieInfo : Window
    {
        int ActionClient;
        int ActionPenaltie;
        int mode;

        public ClientPenaltieInfo(int ActionPenaltie, int ActionClient)
        {
            InitializeComponent();
            this.ActionPenaltie = ActionPenaltie;
            this.ActionClient = ActionClient;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var Car = ConnectDB.DB.Stamp.Select(x => x.StampName).ToList();
            NameCar.ItemsSource = Car;
            var Client = ConnectDB.DB.Client.ToList();
            NameClient.ItemsSource = Client;
            NameClient.DisplayMemberPath = "ClientFullname";
            if (ActionClient > 0)
            {
                NameClient.IsEnabled = false;
                NameClient.SelectedIndex = ActionClient - 1;
            }
            var Ticket = ConnectDB.DB.Article.Select(x=>x.ArticleName).ToList();
            NameArticle.ItemsSource = Ticket;
            if (ActionPenaltie > 0)
            {
                ClientPenalties cp = ConnectDB.DB.ClientPenalties.Where(x => x.CPenaltiesID == ActionPenaltie).FirstOrDefault();
                ViolationDate.Text = cp.CPDateOfViolation.ToString();
                ResolutionDate.Text = cp.CPDateOfTheResolution.ToString();
                NameCar.SelectedValue = cp.CarInfo.Stamp.StampName;
                NameClient.SelectedIndex = cp.ClientID - 1;
                NameArticle.SelectedValue = cp.Article.ArticleName;
                PricePenalties.Text = cp.CPAmountOfTheFine.ToString();
                DiscountedPrice.Text = cp.CPDiscountedAmount.ToString();
                StatusPaid.Text = cp.CPPaidFor.ToString();

            }
        }

        private void ImageClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ImageHide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (ViolationDate.Text != "" & ResolutionDate.Text != "" & NameCar.Text != "" & NameClient.Text != "" & NameArticle.Text != "" & PricePenalties.Text != "" & DiscountedPrice.Text != "" & StatusPaid.Text != "")
            {
                if (ActionPenaltie == 0)
                {
                    ClientPenalties cp = new ClientPenalties();
                    cp.CPDateOfViolation = ViolationDate.SelectedDate.Value;
                    cp.CPDateOfTheResolution = ResolutionDate.SelectedDate.Value;
                    cp.CPCarID = NameCar.SelectedIndex + 1;
                    cp.ClientID = NameClient.SelectedIndex + 1;
                    cp.CPArticleID = NameArticle.SelectedIndex + 1;
                    cp.CPAmountOfTheFine = Convert.ToInt32(PricePenalties.Text);
                    cp.CPDiscountedAmount = Convert.ToInt32(DiscountedPrice.Text);
                    cp.CPPaidFor = StatusPaid.Text;
                    ConnectDB.DB.ClientPenalties.Add(cp);
                    ConnectDB.DB.SaveChanges();
                    SuccessfulWindows sw = new SuccessfulWindows(mode = 3);
                    sw.ShowDialog();
                    this.Close();
                }
                else
                {
                    ClientPenalties cp = ConnectDB.DB.ClientPenalties.Where(x => x.CPenaltiesID == ActionPenaltie).FirstOrDefault();
                    cp.CPDateOfViolation = ViolationDate.SelectedDate.Value;
                    cp.CPDateOfTheResolution = ResolutionDate.SelectedDate.Value;
                    cp.CPCarID = NameCar.SelectedIndex + 1;
                    cp.ClientID = NameClient.SelectedIndex + 1;
                    cp.CPArticleID = NameArticle.SelectedIndex + 1;
                    cp.CPAmountOfTheFine = Convert.ToInt32(PricePenalties.Text);
                    cp.CPDiscountedAmount = Convert.ToInt32(DiscountedPrice.Text);
                    cp.CPPaidFor = StatusPaid.Text;
                    ConnectDB.DB.SaveChanges();
                    SuccessfulWindows sw = new SuccessfulWindows(mode = 6);
                    this.Close();
                }
            }
            else
            {
                ErrorWindows er = new ErrorWindows(mode = 6);
                er.ShowDialog();
            }
        }
    }
}
