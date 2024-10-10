using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Mod4
{
    public interface IBook
    {
        string Title { get; }
        string Author { get; }
        bool IsAvailable { get; }

        // Метод для проверки доступности книги
        bool CheckAvailability();

        // Метод для выдачи книги
        void Issue();
    }

    public class RegularBook : IBook, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isAvailable;
        public string Title { get; private set; }
        public string Author { get; private set; }
        public bool IsAvailable
        {
            get => _isAvailable;
            private set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    OnPropertyChanged();
                }
            }
        }

        public RegularBook(string title, string author, bool isAvailable = true)
        {
            Title = title;
            Author = author;
            IsAvailable = isAvailable;
        }

        public bool CheckAvailability()
        {
            return IsAvailable;
        }

        public void Issue()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                MessageBox.Show($"{Title} выдана.");
            }
            else
                MessageBox.Show($"{Title} недоступна.");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }

    public class ScienceBook : IBook, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isAvailable;
        public string Title { get; private set; }
        public string Author { get; private set; }
        public bool IsAvailable
        {
            get => _isAvailable;
            private set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    OnPropertyChanged();
                }
            }
        }

        public ScienceBook(string title, string author, bool isAvailable = true)
        {
            Title = title;
            Author = author;
            IsAvailable = isAvailable;
        }

        public bool CheckAvailability()
        {
            return IsAvailable;
        }

        public void Issue()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                MessageBox.Show($"{Title} (научная книга) выдана.");
            }
            else
                MessageBox.Show($"{Title} недоступна.");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }

    public partial class MainWindow : Window
    {
        private ObservableCollection<IBook> _books;

        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
            BooksList.ItemsSource = _books;
        }

        private void LoadBooks()
        {
            _books = new ObservableCollection<IBook>
            {
                new RegularBook("Война и Мир", "Лев Толстой"),
                new ScienceBook("Краткая история времени", "Стивен Хокинг"),
                new RegularBook("Преступление и наказание", "Фёдор Достоевский"),
            };
        }

        private void IssueBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItem is IBook selectedBook)
            {
                selectedBook.Issue();
                // Нет необходимости вызывать Items.Refresh(), так как INotifyPropertyChanged и ObservableCollection
                // обеспечивают автоматическое обновление UI
            }
            else
                MessageBox.Show("Пожалуйста, выберите книгу.");
        }
    }
}