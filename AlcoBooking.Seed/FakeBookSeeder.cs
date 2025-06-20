using AlcoBooking.Service;

namespace AlcoBooking.Seed
{
    public static class FakeBookSeeder
    {
        public static void Seed(BookCrudService service, int count)
        {
            var books = FakeBookGenerator.GenerateBooks(count);

            foreach (var book in books)
            {
                service.Add(book.Title, book.Author, book.Year, book.Content);
            }
        }
    }
}
