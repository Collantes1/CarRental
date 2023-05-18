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
using System.Windows.Shapes;

namespace CarRental.Forms.WindowMessage
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationCodeWindow.xaml
    /// </summary>
    public partial class AuthorizationCodeWindow : Window
    {
        int code;
        public AuthorizationCodeWindow()
        {
            InitializeComponent();
            TextBody.Text = "Для авторизации в системе, вам необходимо ввести\rкод, который был отправлен на почту!";
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            code = Convert.ToInt32(CodeTBox.Text);
            User user = ConnectDB.DB.User.Where(x => x.UserUniqueCode == code).FirstOrDefault();
            if (user != null)
            {
                this.Close();
                MainWindow a = new MainWindow();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Authorization a = new Authorization();
            a.ShowDialog();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void IconImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Authorization a = new Authorization();
            a.ShowDialog();
        }

        private void IconHide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
