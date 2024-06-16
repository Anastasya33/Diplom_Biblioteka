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
using Library.Model;
using Library.Pages;

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public static class Globals
        {
            public static int Access;
        }
        public AuthWindow()
        {
            InitializeComponent();

        }
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            if (passVisibility == true)
            {
                passwordBox.Password = ttBox.Text;
            }
            else
            {
                ttBox.Text = passwordBox.Password;
            }
            using (var db = new LibraryDBEntities())
            {
                var auth = db.User.AsNoTracking().FirstOrDefault(m => m.Login == txtEmail.Text && m.Password == passwordBox.Password && m.Password == ttBox.Text);
                if (auth != null)
                {
                    MessageBox.Show("Добро пожаловать!", "Приветствие", MessageBoxButton.OK);

                    Globals.Access = auth.Role_id;

                    MainWindow MW = new MainWindow();
                    MW.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }


        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string telegramUsername = "Yfssj"; // Замените на актуальное имя пользователя в Telegram
            System.Diagnostics.Process.Start($"https://t.me/{telegramUsername}");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new RegPage();
            MyFrame.Navigate(new RegPage());
            Manager.MyFrame = MyFrame;
        }

        bool passVisibility;
        private void PasswordChecker_Click(object sender, RoutedEventArgs e)
        {
            var Check = sender as CheckBox;
            if (Check.IsChecked.Value)
            {
                ttBox.Text = passwordBox.Password;
                ttBox.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Hidden;
                passVisibility = true;
            }
            else
            {
                passwordBox.Password = ttBox.Text;
                ttBox.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Visible;
                passVisibility = false;


            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new Proverkaemail(null));
            Manager.MyFrame = MyFrame;
        }
    }
}
