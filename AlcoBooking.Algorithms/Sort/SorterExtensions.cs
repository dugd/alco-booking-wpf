using AlcoBooking.Algorithms.Sorters;

namespace AlcoBooking.Algorithms.Sort
{
    public static class ISorterExtensions
    {
        public static void SortWith<T>(
            this IList<T> list,
            ISorter<T> sorter,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            if (list == null || list.Count <= 1) return;

            keySelector ??= InferSelector<T>();

            if (!ascending)
            {
                var original = keySelector;
                keySelector = x => new ReverseComparable(original!(x));
            }

            sorter.Sort(list, keySelector!);
        }

        public static void SortWith<T>(
            this T[] array,
            ISorter<T> sorter,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            ((IList<T>)array).SortWith(sorter, keySelector, ascending);
        }

        public static void BubbleSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new BubbleSorter<T>(), keySelector, ascending);
        }

        public static void InsertionSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new InsertionSorter<T>(), keySelector, ascending);
        }

        public static void SelectionSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new SelectionSorter<T>(), keySelector, ascending);
        }

        public static void ShakerSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new ShakerSorter<T>(), keySelector, ascending);
        }

        public static void ShellSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new ShellSorter<T>(), keySelector, ascending);
        }

        public static void MergeSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new MergeSorter<T>(), keySelector, ascending);
        }

        public static void QuickSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new QuickSorter<T>(), keySelector, ascending);
        }

        public static void HeapSort<T>(
            this IList<T> list,
            Func<T, IComparable>? keySelector = null,
            bool ascending = true)
        {
            list.SortWith(new HeapSorter<T>(), keySelector, ascending);
        }

        private static Func<T, IComparable> InferSelector<T>()
        {
            if (typeof(IComparable).IsAssignableFrom(typeof(T)))
                return x => (IComparable)x!;

            throw new InvalidOperationException("Type must implement IComparable or provide keySelector.");
        }

        private class ReverseComparable : IComparable
        {
            private readonly IComparable _inner;
            public ReverseComparable(IComparable inner) => _inner = inner;

            public int CompareTo(object? obj)
            {
                if (obj is ReverseComparable rc)
                    return -_inner.CompareTo(rc._inner);
                return -_inner.CompareTo(obj);
            }
        }
    }
}
