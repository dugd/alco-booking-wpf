using LiteDB;
using AlcoBooking.Core.Models;

namespace AlcoBooking.Data
{
    public class LiteDBContex: IDisposable
    {
        private readonly LiteDatabase _db;
        public ILiteCollection<Book> Books { get; }

        public LiteDBContex(string databasePath = "books.db") {
            _db = new LiteDatabase(databasePath);
            Books = _db.GetCollection<Book>("books");
            Books.EnsureIndex(b => b.Id);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
