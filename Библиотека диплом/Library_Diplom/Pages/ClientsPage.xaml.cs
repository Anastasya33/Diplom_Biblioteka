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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            IntToStringConvClients converter = new IntToStringConvClients();
            Resources.Add("IntToStringConvClients", converter);
            Update_Table();
        }

        void Update_Table()
        {
            DGrid.ItemsSource = LibraryDBEntities.GetContext().Peoples.ToList();
        }

        private void Add_Client(object sender, RoutedEventArgs e)
        {
            AddNewClientWindow ANCW = new AddNewClientWindow(null);
            ANCW.ShowDialog();
            Update_Table();
        }

        private void Edit_Client(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewClientWindow ANCW = new AddNewClientWindow(DGrid.SelectedItem as Peoples);
                ANCW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите редактируемого клиенита!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            Update_Table();
        }

        private void Del_Client(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Peoples>().ToList();

                foreach (var check in Removing)
                {
                    if (LibraryDBEntities.GetContext().Ordered_Books.FirstOrDefault(n => n.People_ID == check.ID) != null)
                    {
                        MessageBox.Show($"Во взятых книгах указывается клиент с именем {check.FullName}, для удаление очистите записи с данным клиентом!",
                                        "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                if (MessageBox.Show($"Вы точно хотите удалить следующие {Removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var rem in Removing)
                        {
                            LibraryDBEntities.GetContext().Peoples.Remove(rem);
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
                MessageBox.Show("Выберите удаляемого клиента!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
