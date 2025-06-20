using AlcoBooking.Core.Models;
using Bogus;

namespace AlcoBooking.Seed
{
    public static class FakeBookGenerator
    {
        public static IList<Book> GenerateBooks(int count)
        {
            var random = new Random();
            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(3, 2))
                .RuleFor(b => b.Author, f => f.Person.FullName)
                .RuleFor(b => b.Year, _ => random.Next(1970, 2025))
                .RuleFor(b => b.Content, f => f.Lorem.Text());

            var books = bookFaker.Generate(count);
            return books;
        }
    }
}
