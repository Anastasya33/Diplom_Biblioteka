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
    /// Логика взаимодействия для AddNewBookWindow.xaml
    /// </summary>
    public partial class AddNewBookWindow : Window
    {
        Book _currentBook = new Book();
        public AddNewBookWindow(Book currentBook)
        {
            InitializeComponent();
            if (currentBook != null)
            {
                _currentBook = currentBook;
                titleW.Text = "Редактирование книги";
                addBTN.Content = "Сохранить";
            }
            DataContext = _currentBook;
            genreB.ItemsSource = LibraryDBEntities.GetContext().Genres.ToList();
        }

        private void Add_New_Book(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (nameB.Text == "")
                Errors.AppendLine("Укажите название книги!");
            if (authorB.Text == "")
                Errors.AppendLine("Укажите автора книги!");
            if (genreB.Text == "")
                Errors.AppendLine("Укажите жанр книги!");
            if (yearB.Text == "")
                Errors.AppendLine("Укажите год книги!");
            if (countB.Text == "")
                Errors.AppendLine("Укажите количество книг!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currentBook.ID == 0)
            {
                _currentBook.Avaliability = true;
                LibraryDBEntities.GetContext().Book.Add(_currentBook);
            }

            try
            {
                LibraryDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Книга сохранена", "Успех", MessageBoxButton.OK);
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
