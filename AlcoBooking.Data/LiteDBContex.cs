using LiteDB;
using AlcoBooking.Core.Models;

namespace AlcoBooking.Data
{
    public class LiteDbContext : IDisposable
    {
        private readonly LiteDatabase _database;
        public ILiteCollection<Book> Books => _database.GetCollection<Book>("books");

        public LiteDbContext(string databasePath = "AlcoBooking.db")
        {
            _database = new LiteDatabase(databasePath);

            Books.EnsureIndex(b => b.Id);
            Books.EnsureIndex(b => b.Title);
            Books.EnsureIndex(b => b.Author);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
