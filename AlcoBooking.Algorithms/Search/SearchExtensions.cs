using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoBooking.Algorithms.Search
{
    public static class SearchExtensions
    {
        public static int LinearSearch<T>(this IList<T> list, T value) where T : IEquatable<T>
        {
            return list.LinearSearch(x => x.Equals(value));
        }

        public static int LinearSearch<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null || predicate == null)
                return -1;

            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    return i;
            }

            return -1;
        }

        public static int BinarySearch<T>(
            this IList<T> list,
            T key,
            IComparer<T>? comparer = null,
            bool ascending = true)
        {
            return list.ToArray().BinarySearch(key, comparer, ascending);
        }

        public static int BinarySearchBy<T, TKey>(
            this IList<T> list,
            TKey key,
            Func<T, TKey> keySelector,
            IComparer<TKey>? comparer = null,
            bool ascending = true)
        {
            if (list == null || list.Count == 0)
                return -1;

            comparer ??= Comparer<TKey>.Default;
            int left = 0, right = list.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                var midKey = keySelector(list[mid]);
                int cmp = comparer.Compare(midKey, key);

                if (cmp == 0) return mid;

                if (ascending)
                {
                    if (cmp < 0) left = mid + 1;
                    else right = mid - 1;
                }
                else
                {
                    if (cmp > 0) left = mid + 1;
                    else right = mid - 1;
                }
            }

            return -1;
        }

        public static int InterpolationSearch(this IList<int> list, int key)
        {
            if (list == null || list.Count == 0)
                return -1;

            int low = 0, high = list.Count - 1;

            while (low <= high && key >= list[low] && key <= list[high])
            {
                if (list[high] == list[low])
                {
                    return list[low] == key ? low : -1;
                }

                int pos = low + (key - list[low]) * (high - low) / (list[high] - list[low]);

                if (pos < 0 || pos >= list.Count)
                    return -1;

                if (list[pos] == key) return pos;
                if (list[pos] < key) low = pos + 1;
                else high = pos - 1;
            }

            return -1;
        }

        public static int InterpolationSearch(this IList<long> list, long key)
        {
            if (list == null || list.Count == 0)
                return -1;

            int low = 0, high = list.Count - 1;

            while (low <= high && key >= list[low] && key <= list[high])
            {
                if (list[high] == list[low])
                {
                    return list[low] == key ? low : -1;
                }

                int pos = (int)(low + (key - list[low]) * (high - low) / (list[high] - list[low]));

                if (pos < 0 || pos >= list.Count)
                    return -1;

                if (list[pos] == key) return pos;
                if (list[pos] < key) low = pos + 1;
                else high = pos - 1;
            }

            return -1;
        }

        public static int InterpolationSearchBy<T>(
            this IList<T> list,
            int key,
            Func<T, int> keySelector)
        {
            if (list == null || list.Count == 0)
                return -1;

            int low = 0, high = list.Count - 1;

            while (low <= high &&
                   key >= keySelector(list[low]) &&
                   key <= keySelector(list[high]))
            {
                int lowKey = keySelector(list[low]);
                int highKey = keySelector(list[high]);

                if (highKey == lowKey)
                    return lowKey == key ? low : -1;

                int pos = low + (key - lowKey) * (high - low) / (highKey - lowKey);

                if (pos < 0 || pos >= list.Count)
                    return -1;

                int posKey = keySelector(list[pos]);

                if (posKey == key) return pos;
                if (posKey < key) low = pos + 1;
                else high = pos - 1;
            }

            return -1;
        }
    }
}
