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
    /// Логика взаимодействия для ErrorWindows.xaml
    /// </summary>
    public partial class ErrorWindows : Window
    {
        int mode;
        public ErrorWindows(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (mode)
            {
                case 1:
                    TextBody.Text = "Клиент не был выбран. Выберите клиента из списка!";
                    break;
                case 2:
                    TextBody.Text = "Штраф не был выбран. Выберите штраф из списка!";
                    break;
                case 3:
                    TextBody.Text = "Аренда не была выбрана. Выберите аренду из списка!";
                    break;
                case 4:
                    TextBody.Text = "Файл не был выбран. Выберите файл из списка!";
                    break;
                case 5:
                    TextBody.Text = "Заполнены не все данные в разделе основной информации!";
                    break;
                case 6:
                    TextBody.Text = "Заполнены не все данные. Заполните все поля в окне";
                    break;
                case 7:
                    break;
                case 8:
                    break;
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IconClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
