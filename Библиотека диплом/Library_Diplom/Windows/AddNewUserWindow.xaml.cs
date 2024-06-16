using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewUserWindow.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        User _currentUser = new User();
        public AddNewUserWindow(User currentUser)
        {
            InitializeComponent();
            if (currentUser != null)
            {
                _currentUser = currentUser;
                titleW.Text = "Редактирование пользователя";
                addBTN.Content = "Сохранить";
            }
            DataContext = _currentUser;

            roleU.ItemsSource = LibraryDBEntities.GetContext().Role.ToList();
        }

        private void Add_New_User(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (nameU.Text == "")
                Errors.AppendLine("Укажите ФИО пользователя!");
            if (loginU.Text == "")
                Errors.AppendLine("Укажите логин пользователя!");
            if (passU.Text == "")
                Errors.AppendLine("Укажите пароль пользователя!");
            if (roleU.SelectedValue == null)
                Errors.AppendLine("Укажите роль пользователя!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currentUser.ID == 0)
            {
                LibraryDBEntities.GetContext().User.Add(_currentUser);
            }

            try
            {
                LibraryDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь сохранён", "Успех", MessageBoxButton.OK);
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
