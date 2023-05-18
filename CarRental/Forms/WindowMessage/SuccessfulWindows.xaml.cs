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
    /// Логика взаимодействия для SuccessfulWindows.xaml
    /// </summary>
    public partial class SuccessfulWindows : Window
    {
        int mode;
        public SuccessfulWindows(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (mode)
            {
                case 1:
                    TextBody.Text = "Пользователь был успешно добавлен в систему!";
                    break;
                case 2:
                    TextBody.Text = "Файл был успешно добавлен в систему!";
                    break;
                case 3:
                    TextBody.Text = "Штраф был успешно добавлен в систему!";
                    break;
                case 4:
                    TextBody.Text = "Сообщение успешно отправлено клиенту!";
                    break;
                case 5:
                    TextBody.Text = "Данные о пользователе успешно обновлены!";
                    break;
                case 6:
                    TextBody.Text = "Данные о файле успешно обновлены!";
                    break;
                case 7:
                    TextBody.Text = "Данные о штрафе успешно обновлены!";
                    break;
                case 8:
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void IconClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
