using AlcoBooking.Core.Models;

namespace AlcoBooking.Service
{
    public class BookSearchService
    {
        private readonly Dictionary<string, Dictionary<object, Book>> _indexes = new();

        public BookSearchService(IEnumerable<Book> books, IEnumerable<string> indexFields)
        {
            BuildIndexes(books, indexFields);
        }

        public void BuildIndexes(IEnumerable<Book> books, IEnumerable<string> fields)
        {
            _indexes.Clear();

            foreach (var field in fields)
            {
                var prop = typeof(Book).GetProperty(field);
                if (prop == null || !typeof(IComparable).IsAssignableFrom(prop.PropertyType)) continue;

                var dict = new Dictionary<object, Book>();
                foreach (var book in books)
                {
                    var value = prop.GetValue(book);
                    if (value != null && !dict.ContainsKey(value))
                        dict[value] = book;
                }

                _indexes[field] = dict;
            }
        }

        public Book? Search(string fieldName, string value)
        {
            if (!_indexes.ContainsKey(fieldName)) return null;

            var prop = typeof(Book).GetProperty(fieldName);
            if (prop == null) return null;

            try
            {
                object? key = Convert.ChangeType(value, prop.PropertyType);
                return key != null && _indexes[fieldName].TryGetValue(key, out var book) ? book : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
