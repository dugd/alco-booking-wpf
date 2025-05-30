using AlcoBooking.Core.Models;

namespace AlcoBooking.Data
{
    public class BookRepository
    {
        private readonly LiteDBContex _context;
        public BookRepository(LiteDBContex context) => _context = context;

        public Book Create(Book book)
        {
            _context.Books.Insert(book);
            return book;
        }

        public bool Update(Book book)
        {
            return _context.Books.Update(book);
        }

        public bool Delete(Book book)
        {
            return _context.Books.Delete(book.Id);
        }

        public Book? GetById(Guid id)
        {
            return _context.Books.FindById(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.FindAll();
        }
    }
}
