using AlcoBooking.Algorithms.Search;
using AlcoBooking.Core.Models;

namespace AlcoBooking.AlcoTester.Algorithms
{
    public class SearchTest
    {
        private static Book[] Books => new[]
        {
            new Book { Title = "A", Year = 2005 },
            new Book { Title = "B", Year = 1999 },
            new Book { Title = "C", Year = 2010 },
            new Book { Title = "D", Year = 2001 }
        };

        private static int[] SortedIntsAsc => new[] { 1, 3, 5, 8, 12 };
        private static int[] SortedIntsDesc => new[] { 12, 8, 5, 3, 1 };
        private static long[] SortedLongs => SortedIntsAsc.Select(i => (long)i).ToArray();

        [Fact]
        public void LinearSearch_Book_ByPredicate_FindsCorrect()
        {
            int pos = Books.LinearSearch(b => b.Title == "D");
            Assert.NotEqual(-1, pos);
            Assert.Equal("D", Books[pos].Title);
        }

        [Fact]
        public void LinearSearch_IntArray_FindsCorrect()
        {
            int pos = SortedIntsAsc.LinearSearch(5);
            Assert.Equal(2, pos);
        }

        [Fact]
        public void LinearSearch_IntArray_NotFound_ReturnsMinusOne()
        {
            int pos = SortedIntsAsc.LinearSearch(99);
            Assert.Equal(-1, pos);
        }

        [Fact]
        public void BinarySearch_IntArray_Ascending()
        {
            int pos = SortedIntsAsc.BinarySearch(5);
            Assert.Equal(2, pos);
        }

        [Fact]
        public void BinarySearch_IntArray_Descending()
        {
            int pos = SortedIntsDesc.BinarySearch(5, ascending: false);
            Assert.Equal(2, pos);
        }

        [Fact]
        public void BinarySearch_Books_ByYear()
        {
            var sortedBooks = Books.OrderBy(b => b.Year).ToArray();
            int pos = sortedBooks.BinarySearchBy(2005, b => b.Year);
            Assert.Equal("A", sortedBooks[pos].Title);
        }

        [Fact]
        public void BinarySearch_IntArray_NotFound()
        {
            int pos = SortedIntsAsc.BinarySearch(99);
            Assert.Equal(-1, pos);
        }

        [Fact]
        public void InterpolationSearch_IntArray_Finds()
        {
            int pos = SortedIntsAsc.InterpolationSearch(8);
            Assert.Equal(3, pos);
        }

        [Fact]
        public void InterpolationSearch_LongArray_Finds()
        {
            int pos = SortedLongs.InterpolationSearch(5L);
            Assert.Equal(2, pos);
        }

        [Fact]
        public void InterpolationSearchBy_Books_ByYear()
        {
            var sorted = Books.OrderBy(b => b.Year).ToArray();
            int pos = sorted.InterpolationSearchBy(2001, b => b.Year);
            Assert.Equal("D", sorted[pos].Title);
        }

        [Fact]
        public void InterpolationSearch_NotFound()
        {
            int pos = SortedIntsAsc.InterpolationSearch(999);
            Assert.Equal(-1, pos);
        }
    }
}
