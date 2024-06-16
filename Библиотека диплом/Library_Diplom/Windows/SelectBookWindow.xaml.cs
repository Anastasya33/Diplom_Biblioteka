using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для SelectBookWindow.xaml
    /// </summary>
    public partial class SelectBookWindow : Window
    {
        Ordered_Books _currentOrderedB = new Ordered_Books();
        Book selected = new Book();
        public SelectBookWindow(Book selectedBook)
        {
            InitializeComponent();
            fullNameC.ItemsSource = LibraryDBEntities.GetContext().Peoples.ToList();
            selected = selectedBook;
            _currentOrderedB.Book_ID = selectedBook.ID;
            DataContext = _currentOrderedB;
        }

        private void Add_New_Book(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (fullNameC.SelectedValue == null)
                Errors.AppendLine("Укажите ФИО клиента!");
            if (backB.SelectedDate == null)
                Errors.AppendLine("Укажите дату возврата книги!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Вычитание арендованной книги из общего количества и смента статуса если книги кончились
            selected.Quantity -= 1;

            if (selected.Quantity == 0)
                selected.Avaliability = false;

            LibraryDBEntities.GetContext().Ordered_Books.Add(_currentOrderedB);

            try
            {
                LibraryDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK);
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
