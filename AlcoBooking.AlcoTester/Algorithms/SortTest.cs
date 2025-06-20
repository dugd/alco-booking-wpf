using AlcoBooking.Algorithms.Sort;
using AlcoBooking.Algorithms.Sorters;
using AlcoBooking.Core.Models;

namespace AlcoBooking.AlcoTester.Algorithms
{
    public class SortTest
    {
        private static int[] UnsortedInts => new[] { 5, 3, 8, 1, 2 };
        private static Book[] UnsortedBooks => new[]
        {
        new Book { Title = "A", Year = 2005 },
        new Book { Title = "B", Year = 1999 },
        new Book { Title = "C", Year = 2010 },
        new Book { Title = "D", Year = 2001 }
    };

        private static int[] SortedIntsAsc => new[] { 1, 2, 3, 5, 8 };
        private static int[] SortedIntsDesc => new[] { 8, 5, 3, 2, 1 };

        private static int[] PositiveInts => new[] { 3, 1, 4, 2, 0 };
        private static double[] DoubleValues => new[] { 0.42, 0.01, 0.99, 0.53, 0.33 };


        [Theory]
        [MemberData(nameof(SorterCases))]
        public void ISorter_IntArray_SortsAscending(ISorter<int> sorter)
        {
            var data = UnsortedInts.ToArray();
            data.SortWith(sorter);
            Assert.Equal(SortedIntsAsc, data);
        }

        [Theory]
        [MemberData(nameof(SorterCases))]
        public void ISorter_IntArray_SortsDescending(ISorter<int> sorter)
        {
            var data = UnsortedInts.ToArray();
            data.SortWith(sorter, ascending: false);
            Assert.Equal(SortedIntsDesc, data);
        }

        [Theory]
        [MemberData(nameof(SorterCasesBooks))]
        public void ISorter_BookArray_SortsByYear(ISorter<Book> sorter)
        {
            var data = UnsortedBooks.ToArray();
            data.SortWith(sorter, b => b.Year);
            var years = data.Select(b => b.Year).ToArray();
            Assert.Equal(new[] { 1999, 2001, 2005, 2010 }, years);
        }


        [Fact]
        public void BucketSort_Doubles_SortsCorrectly()
        {
            var data = DoubleValues.ToList();
            data.BucketSort(x => x);
            Assert.Equal(DoubleValues.OrderBy(x => x), data);
        }

        [Fact]
        public void CountingSort_PositiveInts_SortsCorrectly()
        {
            var data = PositiveInts.ToList();
            data.CountingSort(x => x);
            Assert.Equal(PositiveInts.OrderBy(x => x), data);
        }

        [Fact]
        public void RadixSort_PositiveInts_SortsCorrectly()
        {
            var data = PositiveInts.ToList();
            data.RadixSort(x => x);
            Assert.Equal(PositiveInts.OrderBy(x => x), data);
        }


        public static IEnumerable<object[]> SorterCases() => new List<object[]>
    {
        new object[] { new BubbleSorter<int>() },
        new object[] { new InsertionSorter<int>() },
        new object[] { new SelectionSorter<int>() },
        new object[] { new ShakerSorter<int>() },
        new object[] { new ShellSorter<int>() },
        new object[] { new HeapSorter<int>() },
        new object[] { new QuickSorter<int>() }
    };

        public static IEnumerable<object[]> SorterCasesBooks() => new List<object[]>
    {
        new object[] { new BubbleSorter<Book>() },
        new object[] { new InsertionSorter<Book>() },
        new object[] { new SelectionSorter<Book>() },
        new object[] { new ShakerSorter<Book>() },
        new object[] { new ShellSorter<Book>() },
        new object[] { new HeapSorter<Book>() },
        new object[] { new QuickSorter<Book>() }
    };
    }
}
