using AlcoBooking.Core.Models;

namespace AlcoBooking.Data
{
    public class BookRepository
    {
        private readonly LiteDbContext _context;

        public BookRepository(LiteDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll() => _context.Books.FindAll();

        public Book? GetById(Guid id) => _context.Books.FindById(id);

        public Guid Add(Book book)
        {
            return _context.Books.Insert(book);
        }

        public bool Update(Book book)
        {
            return _context.Books.Update(book);
        }

        public bool Delete(Guid id) => _context.Books.Delete(id);

        public IEnumerable<Book> FindByTitle(string title) =>
            _context.Books.Find(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
    }
}
