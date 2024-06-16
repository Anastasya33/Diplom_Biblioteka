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
    /// Логика взаимодействия для AddNewClientWindow.xaml
    /// </summary>
    public partial class AddNewClientWindow : Window
    {
        Peoples _currentClient = new Peoples();
        public AddNewClientWindow(Peoples currentClient)
        {
            InitializeComponent();
            if (currentClient != null)
            {
                _currentClient = currentClient;
                titleC.Text = "Редактирование клиента";
                addBTN.Content = "Сохранить";
            }
            DataContext = _currentClient;
        }

        private void Add_New_Client(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (fullNameC.Text == "")
                Errors.AppendLine("Укажите ФИО клиента!");
            if (emailC.Text == "")
                Errors.AppendLine("Укажите Email клинета!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currentClient.ID == 0)
            {
                LibraryDBEntities.GetContext().Peoples.Add(_currentClient);
            }

            try
            {
                LibraryDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Клиент сохранён", "Успех", MessageBoxButton.OK);
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
