namespace AlcoBooking.Algorithms.Sorters
{
    public class HeapSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            int n = items.Count;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(items, keySelector, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                (items[0], items[i]) = (items[i], items[0]);
                Heapify(items, keySelector, i, 0);
            }
        }

        private void Heapify(IList<T> items, Func<T, IComparable> keySelector, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && keySelector(items[left]).CompareTo(keySelector(items[largest])) > 0)
                largest = left;

            if (right < n && keySelector(items[right]).CompareTo(keySelector(items[largest])) > 0)
                largest = right;

            if (largest != i)
            {
                (items[i], items[largest]) = (items[largest], items[i]);
                Heapify(items, keySelector, n, largest);
            }
        }
    }
}
