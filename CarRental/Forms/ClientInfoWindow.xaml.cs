using CarRental.Classes.Entity;
using CarRental.Classes;
using CarRental.Forms.PageFrame;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
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
using System.Windows.Forms;
using RadioButton = System.Windows.Controls.RadioButton;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using CarRental.Forms.WindowMessage;

namespace CarRental.Forms
{
    /// <summary>
    /// Логика взаимодействия для ClientInfoWindow.xaml
    /// </summary>
    public partial class ClientInfoWindow : Window
    {
        int ActionClient;
        int IDDivision;
        int ActionFile;
        int ActionPenaltie;
        string searchFiels;
        string searchPenalties;
        string searchmail;
        byte[] fileData;
        int mode = 0;

        public ClientInfoWindow(int ActionClient)
        {
            InitializeComponent();
            this.ActionClient = ActionClient;
            RadioButton rb = new RadioButton();
            rb.Checked += FemaleGenderClient_Checked;
            var PenaltiesInfo = CarRentalEntities.GetContext().ClientPenalties.Where(x=> x.ClientID == ActionClient).ToList();
            PenaltiesClientGrid.ItemsSource = PenaltiesInfo;
            var FielsInfo = CarRentalEntities.GetContext().ClientFiles.Where(x=>x.ClientID == ActionClient).ToList();
            FielsClientGrid.ItemsSource = FielsInfo;
            var MessagesInfo = CarRentalEntities.GetContext().Messages.Where(x => x.MsgClientID == ActionClient).ToList();
            MessagesClientGrid.ItemsSource = MessagesInfo;
        }

        private void ImageClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ImageHide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Заполнение ComboBox Division
            var Division = ConnectDB.DB.DivisionsFMS.Select(x => x.DivisionName).ToList();
            DivisionName.ItemsSource = Division;
            if (ActionClient == 0)
            {
                PhotoClient.Source = BitmapToImageSource(Properties.Resources.no_pictures);
                fileData = ImageToByteArray(Properties.Resources.no_pictures);
            }
            else
            {
                Client cl = ConnectDB.DB.Client.Where(x => x.ClientID == ActionClient).FirstOrDefault();
                SurnameCLient.Text = cl.ClientSurname;
                NameClient.Text = cl.ClientName;
                PatronymicClient.Text = cl.ClientPatronymic;
                if (cl.ClientGender == "Мужской")
                {
                    MaleGenderClient.IsChecked = true;
                }
                else if (cl.ClientGender == "Женский")
                {
                    FemaleGenderClient.IsChecked = true;
                }
                BirthdayClient.Text = cl.ClientBirthDate.ToString();
                BirthPlaceClient.Text = cl.ClientBirthPlace;
                WorkPlaceClient.Text = cl.ClientWorkPlace;
                if (cl.ClientSendingSMS == true)
                {
                    SendingSMSCheckBox.IsChecked = true;
                }
                if (cl.ClientBlackList == true)
                {
                    BlackListCheckBox.IsChecked = true;
                }
                NumberPhoneClient.Text = cl.ClientNumberPhone;
                OtherNumberPhoneClient.Text = cl.ClientOtherNumberPhone;
                EmailClient.Text = cl.ClientEmail;
                OtherInfoClient.Text = cl.ClientOtherInfo;
                if (cl.ClientPhoto == null)
                {
                    PhotoClient.Source = BitmapToImageSource(Properties.Resources.no_pictures);
                    fileData = ImageToByteArray(Properties.Resources.no_pictures);
                }
                else
                {
                    fileData = cl.ClientPhoto;
                    var bitmap = new BitmapImage();
                    MemoryStream ms = new MemoryStream(fileData);
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    PhotoClient.Source = bitmap;

                }
                Passports pass = ConnectDB.DB.Passports.Where(x => x.ClientID == ActionClient).FirstOrDefault();
                CountryOfIssuePassport.Text = pass.PCountryOfIssue;
                SeriesPassport.Text = pass.PSeries.ToString();
                NumberPassport.Text = pass.PNumber.ToString();
                DivisionName.SelectedValue = pass.DivisionsFMS.DivisionName;
                DivisionCode.Text = pass.DivisionsFMS.DivisionCode;
                DateOfIssuePassport.Text = pass.PDateOfIssue.ToString();
                RegistrationPassport.Text = pass.PRegistration;
                DriverLicense dl = ConnectDB.DB.DriverLicense.Where(x => x.ClientID == ActionClient).FirstOrDefault();
                NumberDriverLicense.Text = dl.DLNumber.ToString();
                IssuedDriverLicense.Text = dl.DLIssued;
                DateOfIssueDriverLicense.Text = dl.DLDateOfIssue.ToString();
                ValidDriverLicense.Text = dl.DLValid.ToString();
            }
        }


        private void FemaleGenderClient_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
        }

        private void MaleGenderClient_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
        }

        private void ButtonInfoRent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddFile_Click(object sender, RoutedEventArgs e)
        {
            ClientFileInfo a = new ClientFileInfo(ActionFile = 0, ActionClient);
            a.ShowDialog();
        }

        private void ButtonEditFile_Click(object sender, RoutedEventArgs e)
        {
            ClientFiles cf = (ClientFiles)FielsClientGrid.SelectedItem;
            if ((ClientFiles)FielsClientGrid.SelectedItem == null)
            {
                ErrorWindows er = new ErrorWindows(mode = 4);
                er.ShowDialog();
            }
            else
            {
                ClientFileInfo a = new ClientFileInfo(ActionFile = cf.ClientFileID, ActionClient);
                a.ShowDialog();
            }
        }

        private void ButtonInfoFile_Click(object sender, RoutedEventArgs e)
        {
            ClientFiles cfid = (ClientFiles)FielsClientGrid.SelectedItem;
            if ((ClientFiles)FielsClientGrid.SelectedItem == null)
            {
                ErrorWindows er = new ErrorWindows(mode = 4);
                er.ShowDialog();
            }
            else
            {
                ActionFile = cfid.ClientFileID;
                ClientFiles cf = ConnectDB.DB.ClientFiles.Where(x => x.ClientFileID == ActionFile).FirstOrDefault();
                string tempFileName = Path.GetFileName(cf.CFileName);
                File.WriteAllBytes(tempFileName, cf.CFileData);
                Process.Start(tempFileName);
            }
        }
        private void ButtonUpdateFileGrid_Click(object sender, RoutedEventArgs e)
        {
            FielsClientGrid.ItemsSource = ConnectDB.DB.ClientFiles.Where(x => x.ClientID == ActionClient).ToList();
        }

        private void ButtonUpdatePenalties_Click(object sender, RoutedEventArgs e)
        {
            PenaltiesClientGrid.ItemsSource = ConnectDB.DB.ClientPenalties.Where(x => x.ClientID == ActionClient).ToList();
        }

        private void ButtonUpdateMailGrid_Click(object sender, RoutedEventArgs e)
        {
            MessagesClientGrid.ItemsSource = ConnectDB.DB.Messages.Where(x => x.MsgClientID == ActionClient).ToList();
        }

        private void ButtonAddPenalties_Click(object sender, RoutedEventArgs e)
        {
            ClientPenaltieInfo a = new ClientPenaltieInfo(ActionPenaltie = 0, ActionClient);
            a.ShowDialog();

        }

        private void ButtonEditPenalties_Click(object sender, RoutedEventArgs e)
        {
            ClientPenalties cf = (ClientPenalties)PenaltiesClientGrid.SelectedItem;
            if ((ClientPenalties)PenaltiesClientGrid.SelectedItem == null)
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

        private void ButtonSaveClient_Click(object sender, RoutedEventArgs e)
        {
            // Проверка ввода
            if (SurnameCLient.Text != "" && NameClient.Text != "" && PatronymicClient.Text != "" && BirthdayClient.Text != "" && BirthPlaceClient.Text != "" && WorkPlaceClient.Text != "" && NumberPhoneClient.Text != "" && EmailClient.Text != "")
            {
                if (ActionClient == 0)
                {
                    Client client = new Client();
                    client.ClientSurname = SurnameCLient.Text;
                    client.ClientName = NameClient.Text;
                    client.ClientPatronymic = PatronymicClient.Text;
                    if (MaleGenderClient.IsChecked == true)
                    {
                        client.ClientGender = "Мужской";
                    }
                    else if (FemaleGenderClient.IsChecked == true)
                    {
                        client.ClientGender = "Женский";
                    }
                    client.ClientBirthDate = BirthdayClient.SelectedDate.Value;
                    client.ClientBirthPlace = BirthPlaceClient.Text;
                    client.ClientWorkPlace = WorkPlaceClient.Text;
                    if (BlackListCheckBox.IsChecked == true)
                    {
                        client.ClientBlackList = true;
                    }
                    else
                    {
                        client.ClientBlackList = false;
                    }
                    if (SendingSMSCheckBox.IsChecked == true)
                    {
                        client.ClientSendingSMS = true;
                    }
                    else
                    {
                        client.ClientSendingSMS = false;
                    }
                    client.ClientNumberPhone = NumberPhoneClient.Text;
                    client.ClientOtherNumberPhone = OtherNumberPhoneClient.Text;
                    client.ClientEmail = EmailClient.Text;
                    client.ClientOtherInfo = OtherInfoClient.Text;
                    client.ClientUserAdd = 1;
                    client.ClientUserAddDate = DateTime.Now;
                    client.ClientPhoto = fileData;
                    ConnectDB.DB.Client.Add(client);
                    ConnectDB.DB.SaveChanges();
                    //Паспортные данные
                    Passports pass = new Passports();
                    pass.ClientID = client.ClientID;
                    pass.PCountryOfIssue = CountryOfIssuePassport.Text;
                    pass.PSeries = System.Convert.ToInt32(SeriesPassport.Text);
                    pass.PNumber = System.Convert.ToInt32(NumberPassport.Text);
                    pass.PGivenID = DivisionName.SelectedIndex + 1;
                    pass.PDateOfIssue = DateOfIssuePassport.SelectedDate.Value;
                    pass.PRegistration = RegistrationPassport.Text;
                    ConnectDB.DB.Passports.Add(pass);
                    ConnectDB.DB.SaveChanges();
                    //Водительское удостоверение
                    DriverLicense dl = new DriverLicense();
                    dl.ClientID = client.ClientID;
                    dl.DLNumber = Convert.ToInt32(NumberDriverLicense.Text);
                    dl.DLIssued = IssuedDriverLicense.Text;
                    dl.DLDateOfIssue = DateOfIssueDriverLicense.SelectedDate.Value;
                    dl.DLValid = ValidDriverLicense.SelectedDate.Value;
                    ConnectDB.DB.DriverLicense.Add(dl);
                    ConnectDB.DB.SaveChanges();
                    SuccessfulWindows sw = new SuccessfulWindows(mode = 1);
                    sw.ShowDialog();
                    this.Close();
                }
                else
                {
                    Client client = ConnectDB.DB.Client.Where(x => x.ClientID == ActionClient).FirstOrDefault();
                    client.ClientSurname = SurnameCLient.Text;
                    client.ClientName = NameClient.Text;
                    client.ClientPatronymic = PatronymicClient.Text;
                    if (MaleGenderClient.IsChecked == true)
                    {
                        client.ClientGender = "Мужской";
                    }
                    else if (FemaleGenderClient.IsChecked == true)
                    {
                        client.ClientGender = "Женский";
                    }
                    client.ClientBirthDate = BirthdayClient.SelectedDate.Value;
                    client.ClientBirthPlace = BirthPlaceClient.Text;
                    client.ClientWorkPlace = WorkPlaceClient.Text;
                    if (BlackListCheckBox.IsChecked == true)
                    {
                        client.ClientBlackList = true;
                    }
                    else
                    {
                        client.ClientBlackList = false;
                    }
                    if (SendingSMSCheckBox.IsChecked == true)
                    {
                        client.ClientSendingSMS = true;
                    }
                    else
                    {
                        client.ClientSendingSMS = false;
                    }
                    client.ClientNumberPhone = NumberPhoneClient.Text;
                    client.ClientOtherNumberPhone = OtherNumberPhoneClient.Text;
                    client.ClientEmail = EmailClient.Text;
                    client.ClientOtherInfo = OtherInfoClient.Text;
                    client.ClientPhoto = fileData;
                    client.ClientUserAdd = 1;
                    client.ClientUserAddDate = DateTime.Now;
                    ConnectDB.DB.SaveChanges();
                    //Паспортные данные
                    Passports pass = ConnectDB.DB.Passports.Where(x => x.ClientID == ActionClient).FirstOrDefault();
                    pass.ClientID = client.ClientID;
                    pass.PCountryOfIssue = CountryOfIssuePassport.Text;
                    pass.PSeries = System.Convert.ToInt32(SeriesPassport.Text);
                    pass.PNumber = System.Convert.ToInt32(NumberPassport.Text);
                    pass.PGivenID = DivisionName.SelectedIndex + 1;
                    pass.PDateOfIssue = DateOfIssuePassport.SelectedDate.Value;
                    pass.PRegistration = RegistrationPassport.Text;
                    ConnectDB.DB.SaveChanges();
                    //Водительское удостоверение
                    DriverLicense dl = ConnectDB.DB.DriverLicense.Where(x => x.ClientID == ActionClient).FirstOrDefault();
                    dl.ClientID = client.ClientID;
                    dl.DLNumber = Convert.ToInt32(NumberDriverLicense.Text);
                    dl.DLIssued = IssuedDriverLicense.Text;
                    dl.DLDateOfIssue = DateOfIssueDriverLicense.SelectedDate.Value;
                    dl.DLValid = ValidDriverLicense.SelectedDate.Value;
                    ConnectDB.DB.SaveChanges();
                    SuccessfulWindows sw = new SuccessfulWindows(mode = 5);
                    sw.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                ErrorWindows er = new ErrorWindows(mode = 5);
                er.ShowDialog();
            }

        }

        private void ButtonAddPhotoClient_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                fileData = File.ReadAllBytes(openFileDialog.FileName);
                PhotoClient.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void ButtonDeletePhotoClient_Click(object sender, RoutedEventArgs e)
        {
            PhotoClient.Source = BitmapToImageSource(Properties.Resources.no_pictures);
            fileData = ImageToByteArray(Properties.Resources.no_pictures);

        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void SearchFielsClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchFiels = SearchFielsClient.Text;
            if (!String.IsNullOrEmpty(searchFiels))
            {
                FielsClientGrid.ItemsSource = ConnectDB.DB.ClientFiles.Where(x => x.CFileName.Contains(searchFiels)).ToList();
            }
            else
            {
                FielsClientGrid.ItemsSource = ConnectDB.DB.ClientFiles.Where(x => x.ClientID == ActionClient).ToList();
            }
        }

        private void SearchPenaltiesClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchPenalties = SearchPenaltiesClient.Text;
            if (!String.IsNullOrEmpty(searchPenalties))
            {
                PenaltiesClientGrid.ItemsSource = ConnectDB.DB.ClientPenalties.Where(x => x.Client.ClientSurname.Contains(searchPenalties) | x.Client.ClientName.Contains(searchPenalties) | x.Client.ClientPatronymic.Contains(searchPenalties)).ToList();
            }
            else
            {
                PenaltiesClientGrid.ItemsSource = ConnectDB.DB.ClientPenalties.Where(x=> x.ClientID == ActionClient).ToList();
            }
        }

        private void SearchMessageClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchmail = SearchMessageClient.Text;
            if (!String.IsNullOrEmpty(searchmail))
            {
                MessagesClientGrid.ItemsSource = ConnectDB.DB.Messages.Where(x => x.MsgTema.Contains(searchmail)).ToList();
            }
            else
            {
                MessagesClientGrid.ItemsSource = ConnectDB.DB.Messages.Where(x => x.MsgClientID == ActionClient).ToList();
            }
        }

        private void ButtonAddMessage_Click(object sender, RoutedEventArgs e)
        {
            ClientMessageInfo a = new ClientMessageInfo(ActionClient);
            a.ShowDialog();
        }

        private void DivisionName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IDDivision = DivisionName.SelectedIndex + 1;
            DivisionsFMS divisionsFMS = ConnectDB.DB.DivisionsFMS.Where(x=> x.DivisionFMSID == IDDivision).FirstOrDefault();
            DivisionCode.Text = divisionsFMS.DivisionCode;
        }
    }
}
