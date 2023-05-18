using CarRental.Classes;
using CarRental.Classes.Entity;
using CarRental.Forms.WindowMessage;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRental.Forms
{
    /// <summary>
    /// Логика взаимодействия для ClientMessageInfo.xaml
    /// </summary>
    public partial class ClientMessageInfo : Window
    {
        int mode;
        int ActionClient;
        string tema;
        string textmessage;

        public ClientMessageInfo(int actionClient)
        {
            InitializeComponent();
            this.ActionClient = actionClient;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ActionClient == 0)
            {
                var Client = ConnectDB.DB.Client.ToList();
                NameClient.ItemsSource = Client;
                NameClient.DisplayMemberPath = "ClientFullname";

            }
            else
            {
                var Client = ConnectDB.DB.Client.ToList();
                NameClient.ItemsSource = Client;
                NameClient.DisplayMemberPath = "ClientFullname";
                NameClient.SelectedIndex = ActionClient - 1;
                NameClient.IsEnabled = false;
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

        private void ButtonSendingMessage_Click(object sender, RoutedEventArgs e)
        {
            if (TemaMessage.Text != "" & NameClient.SelectedValue != null & TextMessage.Text != "")
            {
                if (ActionClient == 0)
                {
                    ActionClient = NameClient.SelectedIndex + 1;
                }
                tema = TemaMessage.Text;
                textmessage = TextMessage.Text;
                var mail = MessageTemplate.CreateMail(ActionClient, tema, textmessage);
                MessageTemplate.SendMail(mail);
                Messages m = new Messages();
                m.MsgSender = "webcarrental24@gmail.com";
                m.MsgClientID = NameClient.SelectedIndex + 1;
                m.MsgTema = tema;
                m.MsgBody = textmessage;
                m.MsgSendingTime = DateTime.Now.Date;
                m.MsgStatus = "Отправлено";
                ConnectDB.DB.Messages.Add(m);
                ConnectDB.DB.SaveChanges();
                SuccessfulWindows sw = new SuccessfulWindows(mode = 4);
                sw.ShowDialog();
                this.Close();
            }
            else
            {
                ErrorWindows er = new ErrorWindows(mode = 6);
                er.ShowDialog();
            }
        }
    }
}
