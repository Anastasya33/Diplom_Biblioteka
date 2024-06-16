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
    /// Логика взаимодействия для BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            IntToStringConvBook converter = new IntToStringConvBook();
            Resources.Add("IntToStringConvBook", converter); // Вызов класса из App.xaml.cs для сменны bit значения
            searchBook.ItemsSource = LibraryDBEntities.GetContext().Genres.ToList();
            Update_Table();
        }

        void Update_Table()
        {
            DGrid.ItemsSource = LibraryDBEntities.GetContext().Book.ToList();
        }

        private void Search_Book_Changed(object sender, SelectionChangedEventArgs e)
        {
            var search = LibraryDBEntities.GetContext().Book.ToList();

            if (searchBook.SelectedIndex > -1)
            {
                search = search.Where(s => s.Genre_ID == (searchBook.SelectedItem as Genres).ID).ToList();
            }

            DGrid.ItemsSource = search;
        }

        private void Add_Book(object sender, RoutedEventArgs e)
        {
            AddNewBookWindow ANBW = new AddNewBookWindow(null);
            ANBW.ShowDialog();
            Update_Table();
        }

        private void Edit_Book(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var selected = DGrid.SelectedItem as Book;
                AddNewBookWindow ANBW = new AddNewBookWindow(selected);
                ANBW.ShowDialog();
                if (selected.Quantity > 0) // проверка, если после редактирования при количестве 0 вписали больше книг, смена статуса доступности
                {
                    selected.Avaliability = true;
                    LibraryDBEntities.GetContext().SaveChanges();
                }
            }
            else
                MessageBox.Show("Выберите редактируемую книгу!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            Update_Table();
        }

        private void Del_Book(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Book>().ToList();

                foreach (var check in Removing)
                {
                    // Проверка, есть ли записи в таблице Ordered_Books c удаляемой книгой, если запись есть, то удалить нельзя
                    if (LibraryDBEntities.GetContext().Ordered_Books.FirstOrDefault(n => n.Book_ID == check.ID) != null)
                    {
                        MessageBox.Show($"Во взятых книгах указывается книга с названием {check.Title}, для удаление очистите записи с данной книгой!",
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
                            LibraryDBEntities.GetContext().Book.Remove(rem);
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
                MessageBox.Show("Выберите удаляемую книгу!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Select_Book(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                // Проверка выбранной книги, если выбранная книга недоступна, то нельзя её взять в аренду
                var selected = DGrid.SelectedItem as Book;
                if (selected.Avaliability == false)
                {
                    MessageBox.Show("Книг больше нет!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SelectBookWindow SBW = new SelectBookWindow(DGrid.SelectedItem as Book);
                SBW.ShowDialog();

                Update_Table();
            }
            else
                MessageBox.Show("Выберите книгу!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Genres_List(object sender, RoutedEventArgs e)
        {
            GenresWindow GW = new GenresWindow();
            GW.ShowDialog();
        }
    }
}
