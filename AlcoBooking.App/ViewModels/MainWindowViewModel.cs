using AlcoBooking.Algorithms.Sort;
using AlcoBooking.Core.Models;
using AlcoBooking.Data;
using AlcoBooking.Service;
using AlcoBooking.Seed;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace AlcoBooking.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly BookCrudService _crudService;
        private BookSearchService _searchService;

        public ObservableCollection<Book> Books { get; } = new();

        private Book? _selectedBook;
        public Book? SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
            }
        }

        public List<string> FieldOptions { get; } = new() { "Title", "Author", "Year" };

        public string SortField { get; set; } = "Title";
        public string SearchField { get; set; } = "Title";
        public bool SortAscending { get; set; } = true;

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand GenerateCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SortCommand { get; }

        public MainWindowViewModel()
        {
            var dbContext = new LiteDbContext("books.db");
            var repo = new BookRepository(dbContext);
            _crudService = new BookCrudService(repo);
            Load();

            AddCommand = new RelayCommand(_ => { AddBook(); Load(); });
            EditCommand = new RelayCommand(_ => { EditBook(); Load(); }, _ => SelectedBook != null);
            DeleteCommand = new RelayCommand(_ =>
            {
                if (SelectedBook != null) _crudService.Remove(SelectedBook.Id);
                Load();
            }, _ => SelectedBook != null);

            GenerateCommand = new RelayCommand(_ =>
            {
                FakeBookSeeder.Seed(_crudService, 100);
                Load();
            });
            SearchCommand = new RelayCommand(_ => SearchBook(), _ => !string.IsNullOrWhiteSpace(SearchText));
            SortCommand = new RelayCommand(_ => SortBooks());
        }

        private void Load()
        {
            Books.Clear();
            var allBooks = _crudService.List().ToList();
            foreach (var b in allBooks)
                Books.Add(b);

            _searchService = new BookSearchService(allBooks, FieldOptions);
        }

        private void AddBook()
        {
            var vm = new EditBookViewModel();
            var window = new EditBookWindow { DataContext = vm, Owner = Application.Current.MainWindow };

            if (window.ShowDialog() == true && vm.Result != null)
            {
                _crudService.Add(vm.Result.Title, vm.Result.Author, vm.Result.Year, vm.Result.Content);
                Load();
            }
        }

        private void EditBook()
        {
            if (SelectedBook == null) return;

            var vm = new EditBookViewModel(SelectedBook);
            var window = new EditBookWindow { DataContext = vm, Owner = Application.Current.MainWindow };

            if (window.ShowDialog() == true && vm.Result != null)
            {
                _crudService.Update(SelectedBook.Id, vm.Result.Title, vm.Result.Author, vm.Result.Year, vm.Result.Content);
                Load();
            }
        }

        private void SearchBook()
        {
            if (_searchService == null || string.IsNullOrWhiteSpace(SearchText)) return;

            var found = _searchService.Search(SearchField, SearchText);

            if (found != null)
            {
                SelectedBook = found;
                MessageBox.Show("Книга знайдена.");
            }
            else
            {
                MessageBox.Show("Книгу не знайдено.");
            }
        }

        private void SortBooks()
        {
            PropertyInfo? prop = typeof(Book).GetProperty(SortField);
            if (prop == null)
            {
                MessageBox.Show($"Неіснуєче поле книги: {SortField}");
                return;
            }

            Func<Book, IComparable> keySelector = b => prop.GetValue(b) as IComparable ?? "";

            var list = Books.ToList();
            list.QuickSort(keySelector, SortAscending);

            Books.Clear();
            foreach (var b in list)
                Books.Add(b);
        }
    }
}
