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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library.Model;
using Library.Windows;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }
        private void Registration(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("asa");
            Manager.MyFrame.Content = null;
        }


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MuseDown(object sender, MouseButtonEventArgs e)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Check = LibraryDBEntities.GetContext().User.FirstOrDefault(x => x.Login == txtlLog.Text);
            if (Check != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                User userObj = new User()
                {
                    Login = txtlLog.Text,
                    FullName = txtBxName.Text,
                    Password = passwordBox.Password,
                    Role_id = 1
                };

                LibraryDBEntities.GetContext().User.Add(userObj);
                LibraryDBEntities.GetContext().SaveChanges();

                Proverkaemail pageReg = new Proverkaemail(userObj.Login);
                this.NavigationService.Navigate(pageReg);

                MessageBox.Show("Подтвердите почту", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void PasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox2.Password) && passwordBox2.Password == passwordBox.Password)
            {
                textPassword2.Visibility = Visibility.Collapsed;
                passwordBox2.Background = Brushes.LightGreen;

                Reg.IsEnabled = true;
            }
            else
            {
                textPassword2.Visibility = Visibility.Visible;
                passwordBox2.Background = Brushes.Red;
                Reg.IsEnabled = false;
            }



        }


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox2.Focus();
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

        private void textName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtBxName.Focus();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxName.Text) && txtBxName.Text.Length > 0)
                NameTextBl.Visibility = Visibility.Collapsed;
            else
                NameTextBl.Visibility = Visibility.Visible;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtlLog.Text) && txtlLog.Text.Length > 0)
                textLog.Visibility = Visibility.Collapsed;
            else
                textLog.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtlLog.Focus();
        }

        private void ButtonTG_Click(object sender, RoutedEventArgs e)
        {
            string telegramUsername = "Yfssj"; // Замените на актуальное имя пользователя в Telegram
            System.Diagnostics.Process.Start($"https://t.me/{telegramUsername}");
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
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
                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                // Закрываем текущее окно (страницу)
                Window.GetWindow(this).Close();
            }
        }
    }
}
