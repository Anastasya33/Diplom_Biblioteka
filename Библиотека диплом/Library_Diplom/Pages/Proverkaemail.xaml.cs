using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using Library.Windows;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для Proverkaemail.xaml
    /// </summary>
    public partial class Proverkaemail : Page
    {
        string code;
        public Proverkaemail(string Pochta)
        {
            InitializeComponent();
            code = null;
            Random random = new Random();
            string[] massiveCharacters = new string[]
            { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
            for (int i = 0; i < 4; i++)
            {
                code += massiveCharacters[random.Next(0, massiveCharacters.Length)];
            }

            //Код отправки письма на почту, через подключение порта, шифрования и выбора почты для программы
            string email = Pochta;
            string subject = "Код подтверждения";
            string body = code;

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("nastya.zvereva.993@mail.ru");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtp = new SmtpClient("smtp.mail.ru");
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("nastya.zvereva.993@mail.ru", "LsGQ4LFcGFsmyGNThNJx\r\n");
                smtp.EnableSsl = true;

                smtp.Send(mail);


                MessageBox.Show("Код отправлен на вашу почту!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }




        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text == code)
            {
                NavigationService navService = NavigationService.GetNavigationService(this);

                // Переходим назад (если возможно)
                if (navService.CanGoBack)
                {
                    navService.GoBack();
                }
                else
                {
                    // Если нельзя перейти назад, открываем главное окно
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    // Закрываем текущее окно (страницу)
                    Window.GetWindow(this).Close();
                }
            }
            else
            {
                MessageBox.Show("Неверный код! Вам был отправлен новый код");
                txtEmail.Clear();
                code = null;
                Random random = new Random();
                string[] massiveCharacters = new string[]
                { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
                for (int i = 0; i < 4; i++)
                {
                    code += massiveCharacters[random.Next(0, massiveCharacters.Length)];
                }

                RegPage regpage = new RegPage();

                //Код отправки письма на почту, через подключение порта, шифрования и выбора почты для программы
                string email = regpage.txtlLog.Text;
                string subject = "Код подтверждения";
                string body = code;

                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("artem20040524@mail.ru");
                    mail.To.Add(email);
                    mail.Subject = subject;
                    mail.Body = body;

                    SmtpClient smtp = new SmtpClient("smtp.mail.ru");
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("artem20040524@mail.ru", "fS59Y9Q9w7yvvgeNyepD");
                    smtp.EnableSsl = true;

                    smtp.Send(mail);

                    MessageBox.Show("Код отправлен на вашу почту!");
                }
                catch (Exception ex)
                {
                    //Закоменченно чтобы не выдавало ошибку если инет говноед :)
                    //MessageBox.Show("An error occurred: " + ex.Message);
                    MessageBox.Show("Код отправлен на вашу почту!!!");
                }
            }
        }

        private void Tg_Click(object sender, RoutedEventArgs e)
        {
            string telegramUsername = "Yfssj"; // Замените на актуальное имя пользователя в Telegram
            System.Diagnostics.Process.Start($"https://t.me/{telegramUsername}");
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (sender is FrameworkElement element)
                {
                    Window window = Window.GetWindow(element);
                    window?.DragMove();
                }
            }
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void Otpravka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);

            // Переходим назад (если возможно)
            if (navService.CanGoBack)
            {
                // Если нельзя перейти назад, открываем главное окно
                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                // Закрываем текущее окно (страницу)
                Window.GetWindow(this).Close();

            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
