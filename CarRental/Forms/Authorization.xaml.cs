using CarRental.Classes.Entity;
using CarRental.Classes;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CarRental.Forms.WindowMessage;

namespace CarRental.Forms
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        string password;
        string login;
        int code;

        public Authorization()
        {
            InitializeComponent();
            ConnectDB.DB = new Classes.Entity.CarRentalEntities();
            if (ConnectDB.DB != null)
            {
                MessageBox.Show("Соединение с сервером установлено", "Связь с БД", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ImageClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ImageHide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            password = PasswordPBox.Password;
            login = LoginTBox.Text;

            if (LoginTBox.Text != "" & PasswordPBox.Password != "")
            {
                User user = ConnectDB.DB.User.Where(x => x.UserLogin == login).FirstOrDefault();
                if (user != null)
                {
                    if (password == user.UserPassword)
                    {
                        var mail = MessageTemplate.CreateUniqueCode(user.UserID);
                        MessageTemplate.SendMail(mail);
                        this.Hide();
                        AuthorizationCodeWindow a = new AuthorizationCodeWindow();
                        a.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неверные данные. Повторите попытку", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели неверные данные. Повторите попытку", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Поля логин и пароль не заполнены. Заполните поля и повторите попытку входа", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
