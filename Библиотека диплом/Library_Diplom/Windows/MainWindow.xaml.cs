using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
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
using Library.Model;
using Library.Pages;
using Library.Windows;

namespace Library
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Разграничение ролей, если админ, то отпавляем на фрейм админа и скрытие кнопок
            // если библеотекарь, то отправляем на фрейм с арендами и вызов отправления сообщений на почту
            // (в отельном потоке чтобы работало приложение в момент отправки)
            if (AuthWindow.Globals.Access == 1)
            {
                MainFrame.Navigate(new OrderedBooksPage());
                Thread sending = new Thread(Send_Email_Message);
                sending.Start();
            }
            else
            {
                MainFrame.Navigate(new AdminPage());
                rdClients.Visibility = Visibility.Hidden;
                rdBooks.Visibility = Visibility.Hidden;
                rdRent.Visibility = Visibility.Hidden;

            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdClients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientsPage());
        }

        private void rdBooks_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BooksPage());
        }

        private void rdRent_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderedBooksPage());
        }

        private void rdExit_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow AW = new AuthWindow();
            AW.Show();
            Close();
        }

        // Метод для отправки сообщений на почту
        void Send_Email_Message()
        {
            var orders = LibraryDBEntities.GetContext().Ordered_Books.Where(b => b.Status == 0 || b.Status == 2).ToList(); // список книг, которые ещё не вернули

            foreach (var order in orders)
            {
                // Проверка даты аренд, сообщение отправляется за три дня до даты возврата или же после этой даты
                DateTime returnDate = (DateTime)order.ReturnBook;
                if (returnDate.AddDays(-3) == DateTime.Today || returnDate.AddDays(-2) == DateTime.Today || returnDate.AddDays(-1) == DateTime.Today || returnDate <= DateTime.Today)
                {
                    string email = order.Peoples.Email;
                    string subject = "Верните книгу в библеотеку";
                    string body = $"Если вы увидели это сообщение, простите, оно было отправленно по ошибке (делаю дипломную работу с отправкой сообщений на почту)!!!!!!\nВы брали книгу ({order.Book.Title}, автор {order.Book.Author}) в библеотеке по адресу ул.Ленина, д.50.\nЕё необходимо вернуть до {order.ReturnBook}, в противном случае придёться платить штраф!";

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
                    }
                    catch (Exception ex)
                    {
                        //Закоменченно чтобы не выдавало ошибку если инет говноед :)
                        //MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
        }
    }
}
