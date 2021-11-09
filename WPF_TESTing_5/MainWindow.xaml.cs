using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WPF_TESTing_5
{
    public partial class MainWindow : Window
    {

        bool Pavlo098;

        //  public List<Book> booksList = new List<Book>(); 

        public bool secondSearch;

        // Эти строки кода///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        Random random = new Random();

        string[] BookNames = { "Как стать супер успешным", "Поверь в себя", "Будь дебилом", "Мир не важен, важен ты", "Атлант расправил плечи" };
        string[] AuthorNames = { "Айн Ренд", "Роберт Кийосаки", "Дейл Карнеги", "Ричард Бренсон", "Гай Навасаки", "Генри Форд" };
        string[] Valuations = { "Плохо", "Нормально", "Хорошо", "Шедевр" };

        //public IEnumerable NewSource { get; private set; }

        //нам понадобятся для случайного заполнения ввода данных./////////////////////////////////////////////////////////////////////////////////


        public MainWindow()
        {
            InitializeComponent();
            GetTable();

            BoolOperator();

        }

        void BoolOperator()
        {
            if (secondSearch == false)
            {
                comboSearch_Second.IsEnabled = false;
                textSearch_Second.IsEnabled = false;
            }
            else
            {
                comboSearch_Second.IsEnabled = true;
                textSearch_Second.IsEnabled = true;
            }
        }

        void GetTable()
        {

            try
            {


                using (BookContext bc = new BookContext())
                {
                    listviewitem.ItemsSource = bc.Books.ToList();

                    using (BookContext db = new BookContext())
                    {
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(c => c.TheBookName.Contains(textSearch.Text))}";
                    }
                }
            }
            catch (Exception exception)
            { MessageBox.Show($"Ошибка: {exception.Message}"); }

        }

        private void buttonAdd_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (BookContext db = new BookContext())
                {

                    Book book = new Book
                    {
                        TheBookName = textTheBookName.Text,
                        TheBookAuthor = textTheBookAuthor.Text,
                        TheBookNumberOfPages = Convert.ToInt32(textTheBookNumberOfPages.Text),
                        TheBookValuation = comboTheBookValuation.Text
                    };


                    db.Books.Add(book);
                    db.SaveChanges();

                    MessageBox.Show("Сработало!");

                    GetTable();
                }
            }
            catch (Exception exception)
            { MessageBox.Show($"Ошибка: {exception.Message}"); }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book deleted;
                using (BookContext db = new BookContext())
                {

                    switch (comboFORdelete.Text)
                    {
                        case "Id":
                            {
                                deleted = new Book { Id = Convert.ToInt32(textDelete.Text) };

                                db.Books.Attach(deleted);
                                db.Books.Remove(deleted);
                                break;
                            }
                        case "Название книги":
                            {

                                deleted = db.Books.Where(x => x.TheBookName == textDelete.Text).Single<Book>();
                                db.Books.Remove(deleted);
                                break;
                            }
                        case "Автор":
                            {

                                deleted = db.Books.Where(x => x.TheBookAuthor == textDelete.Text).Single<Book>();
                                db.Books.Remove(deleted);
                                break;
                            }
                        case "Количество страниц":
                            {
                                int TextDelete = Convert.ToInt32(textDelete.Text);

                                deleted = db.Books.Where(x => x.TheBookNumberOfPages == TextDelete).Single<Book>();
                                db.Books.Remove(deleted);
                                break;
                            }
                        case "Оценка":
                            {
                                deleted = db.Books.Where(x => x.TheBookValuation == textDelete.Text).Single<Book>();
                                db.Books.Remove(deleted);
                                break;
                            }


                    }

                    db.SaveChanges();

                    MessageBox.Show("Object delete completed!");

                    GetTable();
                }
            }
            catch (Exception exception) { MessageBox.Show($"Ошибка: {exception.Message}"); }
        }


        private void buttonUPDATE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (BookContext db = new BookContext())
                {
                    switch (comboIN.Text)
                    {
                        case "Id":
                            {
                                var textin = Convert.ToInt32(textupdateIN.Text);

                                var Update = db.Books.SingleOrDefault(b => b.Id == textin);
                                if (Update != null)
                                {

                                    UpdateHelper(Update);
                                    db.SaveChanges();

                                }

                                break;
                            }
                        case "Название книги":
                            {
                                var textinstring = textupdateIN.Text;

                                var Update = db.Books.SingleOrDefault(b => b.TheBookName == textinstring);

                                if (Update != null)
                                {

                                    UpdateHelper(Update);
                                    db.SaveChanges();

                                }

                                break;
                            }
                        case "Автор":
                            {
                                var textinstring = textupdateIN.Text;

                                var Update = db.Books.SingleOrDefault(b => b.TheBookAuthor == textinstring);
                                if (Update != null)
                                {

                                    UpdateHelper(Update);
                                    db.SaveChanges();

                                }

                                break;
                            }

                        case "Количество страниц":
                            {
                                var textin = Convert.ToInt32(textupdateIN.Text);


                                var Update = db.Books.SingleOrDefault(b => b.TheBookNumberOfPages == textin);
                                if (Update != null)
                                {
                                    UpdateHelper(Update);
                                    db.SaveChanges();

                                }

                                break;
                            }
                        case "Оценка":
                            {
                                var textinstring = textupdateIN.Text;

                                var Update = db.Books.SingleOrDefault(b => b.TheBookValuation == textinstring);
                                if (Update != null)
                                {

                                    UpdateHelper(Update);
                                    db.SaveChanges();

                                }

                                break;
                            }



                    }

                    MessageBox.Show("Файл изменён!");
                    GetTable();
                }
            }
            catch (Exception exception) { MessageBox.Show("Ошибка: " + exception.Message); }
        }

        void UpdateHelper(Book update)
        {

            switch (comboOUT.Text)
            {
                case "Название книги":
                    update.TheBookName = textupdateOUT.Text;
                    break;
                case "Автор":
                    update.TheBookAuthor = textupdateOUT.Text;
                    break;
                case "Количество страниц":
                    update.TheBookNumberOfPages = Convert.ToInt32(textupdateOUT.Text);
                    break;
                case "Оценка":
                    update.TheBookValuation = textupdateOUT.Text;
                    break;

            }
        }

        private void buttonRandom_Click(object sender, RoutedEventArgs e)
        {
            textTheBookName.Text = BookNames[random.Next(0, BookNames.Length)];
            textTheBookAuthor.Text = AuthorNames[random.Next(0, AuthorNames.Length)];
            textTheBookNumberOfPages.Text = (random.Next(123, 654)).ToString();
            comboTheBookValuation.Text = Valuations[random.Next(0, Valuations.Length)];
        }

        private void SEARCHbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (secondSearch == false || textSearch_Second.Text == null)
                {

                    using (BookContext db = new BookContext())
                    {
                        if (textSearch.Text != "")
                        {
                            var select = new Book();
                            IEnumerable<Book> Search = db.Books.ToList();

                            Search = searchHelper(Search);

                            if (Search != null)
                            {
                                listviewitem.ItemsSource = Search;
                            }

                        }
                        else
                        {
                            GetTable();
                        }
                    }
                }

                else if (textSearch.Text == "" && textSearch_Second.Text != "")
                {
                    using (BookContext db = new BookContext())
                    {
                        if (textSearch_Second.Text != "")
                        {
                            var select = new Book();
                            IEnumerable<Book> Search = db.Books.ToList();

                            Search = second_searchHelper(Search);

                            if (Search != null)
                            {
                                listviewitem.ItemsSource = Search;
                            }

                        }
                        else
                        {
                            GetTable();

                        }
                    }
                }

                else
                {
                    using (BookContext db = new BookContext())
                    {
                        if (textSearch.Text != "")
                        {
                            var select = new Book();
                            IEnumerable<Book> Search = db.Books.ToList();

                            Search = searchHelper(Search);
                            Search = second_searchHelper(Search);


                            if (Search != null)
                            {
                                listviewitem.ItemsSource = Search;
                            }



                        }
                        else
                        {
                            GetTable();
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
            }



        }
        IEnumerable<Book> searchHelper(IEnumerable<Book> Search)
        {
            using (BookContext db = new BookContext())
            {
                switch (comboSearch.Text)
                {
                    case "Id":

                        Search = Search.Where(x => x.Id == Convert.ToInt32(textSearch.Text));
                        labelCOUNT.Content = $"Количество элементов: 1/{db.Books.Count()}";
                        break;

                    case "Название книги":
                        Search = Search.Where(x => x.TheBookName.Contains(textSearch.Text));
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(c => c.TheBookName.Contains(textSearch.Text))}/{db.Books.Count()}";
                        break;

                    case "Автор":
                        Search = Search.Where(x => x.TheBookAuthor.Contains(textSearch.Text));
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(c => c.TheBookAuthor.Contains(textSearch.Text))}/{db.Books.Count()}";
                        break;

                    case "Количество страниц":
                        var textPagesCountsearch = Convert.ToInt32(textSearch.Text);
                        Search = Search.Where(x => x.TheBookNumberOfPages == textPagesCountsearch);
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(c => c.TheBookNumberOfPages == textPagesCountsearch)}/{db.Books.Count()}";
                        break;

                    case "Оценка":
                        Search = Search.Where(x => x.TheBookValuation == textSearch.Text);
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(x => x.TheBookValuation == textSearch.Text)}/{db.Books.Count()}";
                        break;
                    default:
                        GetTable();
                        break;

                }
            }
            return Search;
        }

        IEnumerable<Book> second_searchHelper(IEnumerable<Book> Search)
        {
            using (BookContext db = new BookContext())
            {
                switch (comboSearch_Second.Text)
                {
                    case "Id":

                        Search = Search.Where(x => x.Id == Convert.ToInt32(textSearch_Second.Text));
                        labelCOUNT.Content = $"Количество элементов: 1/{db.Books.Count()}";
                        break;

                    case "Название книги":
                        Search = Search.Where(x => x.TheBookName.Contains(textSearch_Second.Text));
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(c => c.TheBookName.Contains(textSearch_Second.Text))}/{db.Books.Count()}";
                        break;

                    case "Автор":
                        Search = Search.Where(x => x.TheBookAuthor.Contains(textSearch_Second.Text));
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(c => c.TheBookAuthor.Contains(textSearch_Second.Text))}/{db.Books.Count()}";
                        break;

                    case "Количество страниц":
                        var textPagesCountsearch = Convert.ToInt32(textSearch_Second.Text);
                        Search = Search.Where(x => x.TheBookNumberOfPages == textPagesCountsearch);
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(c => c.TheBookNumberOfPages == textPagesCountsearch)}/{db.Books.Count()}";
                        break;

                    case "Оценка":
                        Search = Search.Where(x => x.TheBookValuation == textSearch_Second.Text);
                        labelCOUNT.Content = $"Количество элементов: {db.Books.Count(x => x.TheBookValuation == textSearch_Second.Text)}/{db.Books.Count()}";
                        break;
                    default:
                        GetTable();
                        break;

                }
            }

            return Search;
        }



        private void checkBoxSearch_Checked(object sender, RoutedEventArgs e)
        {
            secondSearch = true;
            BoolOperator();
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            secondSearch = false;
            BoolOperator();
        }
        private void checkBox_Indeterminate(object sender, RoutedEventArgs e)
        {

        }

        private void addButtonClear_Click(object sender, RoutedEventArgs e)
        {
            textTheBookName.Text = "";
            textTheBookAuthor.Text = "";
            textTheBookNumberOfPages.Text = "";
            comboTheBookValuation.Text = "";
        }

        private void DeleteTextClear_Click(object sender, RoutedEventArgs e)
        {
            textDelete.Text = "";
            comboFORdelete.Text = "";
        }

        private void updateClearButton_Click(object sender, RoutedEventArgs e)
        {
            comboIN.Text = "";
            comboOUT.Text = "";
            textupdateIN.Text = "";
            textupdateOUT.Text = "";
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string TheBookName { get; set; }
        public string TheBookAuthor { get; set; }
        public int TheBookNumberOfPages { get; set; }
        public string TheBookValuation { get; set; }
    }

    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Db.mdf;Database=TheBooks; Integrated Security=True;");
        }
    }
}
