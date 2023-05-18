using CarRental.Classes.Entity;
using CarRental.Forms.WindowMessage;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
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
using Path = System.IO.Path;

namespace CarRental.Forms
{
    /// <summary>
    /// Логика взаимодействия для ClientFileInfo.xaml
    /// </summary>
    public partial class ClientFileInfo : Window
    {
        byte[] fileData;
        int ActionFile;
        int ActionClient;
        int mode;

        public ClientFileInfo(int ActionFile, int ActionClient)
        {
            InitializeComponent();
            this.ActionFile = ActionFile;
            this.ActionClient = ActionClient;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Заполнение ComboBox Division
            var Division = ConnectDB.DB.Client.ToList();
            ClientFIOComboBox.ItemsSource = Division;
            ClientFIOComboBox.DisplayMemberPath = "ClientFullname";
            if (ActionClient > 0)
            {
                ClientFIOComboBox.SelectedIndex = ActionClient - 1;
                ClientFIOComboBox.IsEnabled = false;
            }
            if (ActionFile == 0)
            {

            }
            else
            {
                ClientFiles cf = ConnectDB.DB.ClientFiles.Where(x => x.ClientFileID == ActionFile).FirstOrDefault();
                NameFile.Text = cf.CFileName;
                DescriptionFile.Text = cf.CFileDescription;
                ClientFIOComboBox.SelectedIndex = cf.ClientID - 1;
                fileData = cf.CFileData;
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

        private void ButtonAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                fileData = File.ReadAllBytes(openFileDialog.FileName);
                NameFile.Text = Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void ButtonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (NameFile.Text != "" & DescriptionFile.Text != "" & ClientFIOComboBox.Text != "")
            {
                if (ActionFile == 0)
                {
                    ClientFiles cf = new ClientFiles();
                    cf.ClientID = ClientFIOComboBox.SelectedIndex + 1;
                    cf.CFileName = NameFile.Text;
                    cf.CFileDescription = DescriptionFile.Text;
                    cf.CFileDate = DateTime.Now.Date;
                    cf.CFileUser = 1;
                    cf.CFileData = fileData;
                    ConnectDB.DB.ClientFiles.Add(cf);
                    ConnectDB.DB.SaveChanges();
                    SuccessfulWindows sw = new SuccessfulWindows(mode = 2);
                    sw.ShowDialog();
                    this.Close();

                }
                else
                {
                    ClientFiles cf = ConnectDB.DB.ClientFiles.Where(x => x.ClientFileID == ActionFile).FirstOrDefault();
                    cf.ClientID = ClientFIOComboBox.SelectedIndex + 1;
                    cf.CFileName = NameFile.Text;
                    cf.CFileDescription = DescriptionFile.Text;
                    cf.CFileDate = DateTime.Now.Date;
                    cf.CFileUser = 1;
                    cf.CFileData = fileData;
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
