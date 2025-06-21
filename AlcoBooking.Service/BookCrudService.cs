using AlcoBooking.Core.Models;
using AlcoBooking.Data;

namespace AlcoBooking.Service
{
    public class BookCrudService
    {
        private readonly BookRepository _repo;

        public BookCrudService(BookRepository repository) => _repo = repository;

        public Book Add(string title, string author, int year, string content)
        {
            Book book = new Book { Title = title, Author = author, Year = year, Content = content };
            _repo.Add(book);
            return book;
        }

        public bool Update(Guid id, string title, string author, int year, string content)
        {
            var book = _repo.GetById(id);
            if (book is null) return false;
            book.Title = title; book.Author = author; book.Year = year; book.Content = content;
            return _repo.Update(book);
        }

        public bool Remove(Guid id)
        {
            var book = _repo.GetById(id);
            if (book is null) return false;
            return _repo.Delete(book.Id);
        }

        public IEnumerable<Book> List() => _repo.GetAll();

        public Book? Get(Guid id) => _repo.GetById(id); 
    }
}
