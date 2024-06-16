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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            Update_Table();
        }

        void Update_Table()
        {
            DGrid.ItemsSource = LibraryDBEntities.GetContext().User.ToList();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            AddNewUserWindow ANUW = new AddNewUserWindow(null);
            ANUW.ShowDialog();
            Update_Table();
        }

        private void Edit_User(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewUserWindow ANUW = new AddNewUserWindow(DGrid.SelectedItem as User);
                ANUW.ShowDialog();
                Update_Table();
            }
            else
                MessageBox.Show("Выберите редактируемого пользователя!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Del_User(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<User>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {Removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var rem in Removing)
                        {
                            LibraryDBEntities.GetContext().User.Remove(rem);
                        }
                        LibraryDBEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удаленны", "Успех", MessageBoxButton.OK);
                        Update_Table();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите удаляемого пользователя!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
