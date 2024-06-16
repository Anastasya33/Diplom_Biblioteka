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

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для GenresWindow.xaml
    /// </summary>
    public partial class GenresWindow : Window
    {
        public GenresWindow()
        {
            InitializeComponent();
            Update_Table();
        }

        void Update_Table()
        {
            DGrid.ItemsSource = LibraryDBEntities.GetContext().Genres.ToList();
        }

        private void Add_Genre(object sender, RoutedEventArgs e)
        {
            AddNewGenreWindow ANGW = new AddNewGenreWindow(null);
            ANGW.ShowDialog();
            Update_Table();
        }

        private void Edit_Genre(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewGenreWindow ANGW = new AddNewGenreWindow(DGrid.SelectedItem as Genres);
                ANGW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите редактируемый жанр!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            Update_Table();
        }

        private void Del_Genre(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Genres>().ToList();

                foreach (var check in Removing)
                {
                    // Проверка, есть ли записи в таблице Ordered_Books c удаляемой книгой, если запись есть, то удалить нельзя
                    if (LibraryDBEntities.GetContext().Book.FirstOrDefault(n => n.Genre_ID == check.ID) != null)
                    {
                        MessageBox.Show($"В книгах указывается книга с жанром {check.Name}, для удаление очистите записи с данным жанром!",
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
                            LibraryDBEntities.GetContext().Genres.Remove(rem);
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
                MessageBox.Show("Выберите удаляемый жанр!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
