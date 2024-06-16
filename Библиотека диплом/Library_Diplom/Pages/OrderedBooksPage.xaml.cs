using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Word = Microsoft.Office.Interop.Word;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderedBooksPage.xaml
    /// </summary>
    public partial class OrderedBooksPage : Page
    {
        public OrderedBooksPage()
        {
            InitializeComponent();
            IntToStringConvClients converter = new IntToStringConvClients();
            Resources.Add("IntToStringConvClients", converter);
            Check_Return_Date();
            Update_Table();
        }

        //Метод для проверки всех аренд со статусом "Не вернул" и проверяет дату возращения
        //Если сегодняшняя дата больше чем дата возврата, статус меняется на "Просрочил"
        void Check_Return_Date()
        {
            var allOrders = LibraryDBEntities.GetContext().Ordered_Books.Where(d => d.Status == 0);

            foreach (var order in allOrders)
            {
                if (order.ReturnBook <= DateTime.Today)
                {
                    order.Status = 2;
                }
            }
            
            LibraryDBEntities.GetContext().SaveChanges();
        }

        void Update_Table()
        {
            DGrid.ItemsSource = LibraryDBEntities.GetContext().Ordered_Books.ToList();
        }

        private void Back_Order(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Selected = DGrid.SelectedItems.Cast<Ordered_Books>().ToList();

                try
                {
                    foreach (var select in Selected)
                    {
                        var days_left = ((DateTime)select.ReturnBook - DateTime.Today).TotalDays;
                        // Поиск книги которая указанна в аренде и добавление +1 в Quantitiy и смена статуса книги Avaliability
                        var book = LibraryDBEntities.GetContext().Book.FirstOrDefault(b => b.ID == select.Book_ID);
                        if (book.Avaliability == false)
                            book.Avaliability = true;

                        book.Quantity += 1;

                        select.Status = 1;
                        if (days_left < 0) // проверка, если клиент просрочил возврат, то пишеться сколько нужно заплатить штраф (дни умножаются на 200)
                        {
                            MessageBox.Show($"Клиент просрочил возврат книги, он должен заплатить штраф в размере {Math.Abs(days_left) * 200}р.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    LibraryDBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Аренда прекращенна", "Успех", MessageBoxButton.OK);
                    Update_Table();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Выберите аренду!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Del_Order(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Ordered_Books>().ToList();

                // Проверка, вернули ли клиент книгу или нет, если нет, то аренду не удалить
                foreach (var rem in Removing)
                {
                    if (rem.Status == 0)
                    {
                        MessageBox.Show("Невозможно удалить, так как клиент не вернул книгу!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                            LibraryDBEntities.GetContext().Ordered_Books.Remove(rem);
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
                MessageBox.Show("Выберите удаляемую аренду!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Orders_report(object sender, RoutedEventArgs e)
        {
            Thread generating_report = new Thread(Generating_Report);
            generating_report.Start();
        }

        void Generating_Report()
        {
            try
            {
                var orderedBooks = LibraryDBEntities.GetContext().Ordered_Books.ToList();
                var backBook = LibraryDBEntities.GetContext().Ordered_Books.Where(b => b.Status == 1).ToList();
                int noBackBook = orderedBooks.Count - backBook.Count;

                // Списки данных которые необходимо вставить, также перечисление меток из документа и названия столбцов
                string[] textsToFind = { "~O_B~", "~B_B~", "~NB_B~" };
                int[] replacements = { orderedBooks.Count, backBook.Count, noBackBook };
                string[] columsName = { "ФИО клиента", "Взятая книга", "Дата возврата", "Статус" };

                var app = new Word.Application();
                app.Visible = false;

                Word.Document doc = app.Documents.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Templates\Library_report.docx");
                Word.Document newDoc = app.Documents.Add();
                newDoc.Content.FormattedText = doc.Content.FormattedText;

                for (int i = 0; i < textsToFind.Length; i++) // Вставка данных в поля ~O_B~, ~B_B~, ~NB_B~
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = textsToFind[i];
                    find.Replacement.Text = Convert.ToString(replacements[i]);
                    find.Execute(Replace: Word.WdReplace.wdReplaceAll);
                }

                Word.Find findT = app.Selection.Find; // Логика для вставки таблицы в поле ~Ordered_Books~
                findT.Text = "~Ordered_Books~";
                findT.Execute();

                app.Selection.ClearFormatting();

                if (findT.Found)
                {
                    Word.Range range = app.Selection.Range;
                    Word.Table newTable = newDoc.Tables.Add(range, orderedBooks.Count + 1, 4);
                    newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle; // Устанавливаем стиль линии для внутренних границ
                    newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle; // Устанавливаем стиль линии для внешних границ

                    // Заполнение заголовков таблицы
                    for (int i = 0; i < columsName.Count(); i++)
                    {
                        newTable.Cell(1, i + 1).Range.Text = columsName[i];
                    }

                    for (int row = 2; row <= orderedBooks.Count + 1; row++)
                    {
                        newTable.Cell(row, 1).Range.Text = orderedBooks[row - 2].Peoples.FullName; // Заполнение столбца "Тип"
                        newTable.Cell(row, 2).Range.Text = orderedBooks[row - 2].Book.Title; // Заполнение столбца "Название"
                        DateTime date = DateTime.ParseExact(orderedBooks[row - 2].ReturnBook.ToString(), "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        string formattedDate = date.ToString("dd.MM.yyyy");
                        newTable.Cell(row, 3).Range.Text = formattedDate; // Заполнение столбца "Дата возврата"
                        if (orderedBooks[row - 2].Status == 1)
                        {
                            newTable.Cell(row, 4).Range.Text = "Вернул"; // Заполнение столбца "Статус"
                        }
                        else
                        {
                            newTable.Cell(row, 4).Range.Text = "Не вернул"; // Заполнение столбца "Статус"
                        }
                    }
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Взятие путя до рабочего стола

                newDoc.SaveAs(path + $@"\Отчёт.docx"); // Создание и сборка полного пути до файла

                doc.Close();
                newDoc.Close();

                app.Quit();

                MessageBox.Show("Отчёт создан на рабочем столе", "Успех", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //Метод для поиска по ФИО, статусу заказа
        private void Search_method(object sender, TextChangedEventArgs e)
        {
            if (SearchTxb.Text == "")
            {
                DGrid.ItemsSource = LibraryDBEntities.GetContext().Ordered_Books.ToList();
                SearchBl.Visibility = Visibility.Visible;
            }
            else if (SearchTxb.Text == "Не вернул")
            {
                SearchBl.Visibility = Visibility.Hidden;
                DGrid.ItemsSource = LibraryDBEntities.GetContext().Ordered_Books.Where(s => s.Status == 0).ToList();
            }
            else if (SearchTxb.Text == "Вернул")
            {
                SearchBl.Visibility = Visibility.Hidden;
                DGrid.ItemsSource = LibraryDBEntities.GetContext().Ordered_Books.Where(s => s.Status == 1).ToList();
            }
            else if (SearchTxb.Text == "Просрочил")
            {
                SearchBl.Visibility = Visibility.Hidden;
                DGrid.ItemsSource = LibraryDBEntities.GetContext().Ordered_Books.Where(s => s.Status == 2).ToList();
            }
            else
            {
                SearchBl.Visibility = Visibility.Hidden;
                DGrid.ItemsSource = LibraryDBEntities.GetContext().Ordered_Books.Where(s => s.Peoples.FullName == SearchTxb.Text || s.Peoples.FullName.Contains(SearchTxb.Text)).ToList();
            }
        }
    }
}
