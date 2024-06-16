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
    /// Логика взаимодействия для AddNewGenreWindow.xaml
    /// </summary>
    public partial class AddNewGenreWindow : Window
    {
        Genres _currentGenre = new Genres();
        public AddNewGenreWindow(Genres selectedGenre)
        {
            InitializeComponent();
            if (selectedGenre != null)
            {
                _currentGenre = selectedGenre;
                titleW.Text = "Редактирование жанра";
                addBTN.Content = "Сохранить";
            }
            DataContext = _currentGenre;
        }

        private void Add_New_Genre(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (nameG.Text == "")
                Errors.AppendLine("Укажите название жанра!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currentGenre.ID == 0)
            {
                LibraryDBEntities.GetContext().Genres.Add(_currentGenre);
            }

            try
            {
                LibraryDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Жанр сохранён", "Успех", MessageBoxButton.OK);
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
