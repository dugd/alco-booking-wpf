namespace AlcoBooking.Algorithms.Sort
{
    public static class SpecificSortExtensions
    {
        public static void CountingSort<T>(
            this IList<T> list,
            Func<T, int> keySelector,
            bool ascending = true)
        {
            if (list.Count <= 1) return;

            int min = list.Min(keySelector);
            int max = list.Max(keySelector);

            int range = max - min + 1;
            var count = new List<T>[range];
            for (int i = 0; i < range; i++) count[i] = new List<T>();

            foreach (var item in list)
                count[keySelector(item) - min].Add(item);

            if (!ascending) Array.Reverse(count);

            int pos = 0;
            foreach (var bucket in count)
                foreach (var item in bucket)
                    list[pos++] = item;
        }

        public static void CountingSort(
            this IList<int> list,
            bool ascending = true)
        {
            list.CountingSort(x => x, ascending);
        }

        public static void RadixSort<T>(
            this IList<T> list,
            Func<T, int> keySelector,
            bool ascending = true)
        {
            if (list.Count <= 1) return;

            int max = list.Max(keySelector);
            int exp = 1;
            while (max / exp > 0)
            {
                CountingSortByDigit(list, keySelector, exp);
                exp *= 10;
            }

            if (!ascending)
                list.ReverseInPlace();
        }

        public static void RadixSort(
            this IList<int> list,
            bool ascending = true)
        {
            list.RadixSort(x => x, ascending);
        }

        private static void CountingSortByDigit<T>(IList<T> list, Func<T, int> keySelector, int exp)
        {
            int n = list.Count;
            var output = new T[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
                count[(keySelector(list[i]) / exp) % 10]++;

            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (int i = n - 1; i >= 0; i--)
            {
                int digit = (keySelector(list[i]) / exp) % 10;
                output[count[digit] - 1] = list[i];
                count[digit]--;
            }

            for (int i = 0; i < n; i++)
                list[i] = output[i];
        }

        public static void BucketSort<T>(
            this IList<T> list,
            Func<T, double> keySelector,
            bool ascending = true)
        {
            int n = list.Count;
            if (n <= 1) return;

            var buckets = new List<T>[n];
            for (int i = 0; i < n; i++)
                buckets[i] = new List<T>();

            foreach (var item in list)
            {
                double key = keySelector(item);
                int index = (int)(key * n);
                if (index == n) index = n - 1;
                buckets[index].Add(item);
            }

            int pos = 0;
            var ordered = ascending ? buckets : buckets.Reverse();
            foreach (var bucket in ordered)
            {
                bucket.SelectionSort(x => keySelector(x), true);
                foreach (var item in bucket)
                    list[pos++] = item;
            }
        }

        public static void BucketSort(
            this IList<double> list,
            bool ascending = true)
        {
            list.BucketSort(x => x, ascending);
        }

        private static void ReverseInPlace<T>(this IList<T> list)
        {
            int i = 0, j = list.Count - 1;
            while (i < j)
            {
                (list[i], list[j]) = (list[j], list[i]);
                i++; j--;
            }
        }
    }
}
