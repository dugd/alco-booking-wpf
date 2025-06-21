using AlcoBooking.Core.Models;

namespace AlcoBooking.App.ViewModels
{
    public class EditBookViewModel : ViewModelBase
    {
        private string _title = "";
        private string _author = "";
        private int _year = DateTime.Now.Year;
        private string _content = "";

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Author
        {
            get => _author;
            set { _author = value; OnPropertyChanged(); }
        }

        public int Year
        {
            get => _year;
            set { _year = value; OnPropertyChanged(); }
        }

        public string Content
        {
            get => _content;
            set { _content = value; OnPropertyChanged(); }
        }

        public Book? Result { get; private set; }

        public EditBookViewModel()
        {
        }

        public EditBookViewModel(Book book)
        {
            Title = book.Title;
            Author = book.Author;
            Year = book.Year;
            Content = book.Content;
        }

        public void Save()
        {
            Result = new Book
            {
                Title = this.Title,
                Author = this.Author,
                Year = this.Year,
                Content = this.Content
            };
        }
    }
}
