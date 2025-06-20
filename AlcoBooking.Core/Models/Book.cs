using System;

namespace AlcoBooking.Core.Models
{
    public class Book : IComparable<Book>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }

        public string Content { get; set; } = string.Empty;

        public int ContentLength => Content.Length;

        public int CompareTo(Book? other)
        {
            if (other == null) return 1;
            return ContentLength.CompareTo(other.ContentLength);
        }
    }
}
