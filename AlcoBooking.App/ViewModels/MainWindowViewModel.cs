using AlcoBooking.Core.Models;
using AlcoBooking.Data;
using AlcoBooking.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AlcoBooking.App.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly BookCrudService _service;
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();
        public Book? SelectedBook { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        
        public MainWindowViewModel()
        {
            var dbContext = new LiteDbContext("books.db");
            var repo = new BookRepository(dbContext);
            _service = new BookCrudService(repo);
            Load();

            AddCommand = new RelayCommand(_ => {; Load(); });
            EditCommand = new RelayCommand(_ => {; Load(); }, _ => SelectedBook != null);
            DeleteCommand = new RelayCommand(_ => {
                if (SelectedBook != null) _service.Remove(SelectedBook.Id);
                Load();
            }, _ => SelectedBook != null);
        }

        private void Load()
        {
            Books.Clear();
            foreach (var b in _service.List())
            {
                Books.Add(b);
            }
        }
    }
}
