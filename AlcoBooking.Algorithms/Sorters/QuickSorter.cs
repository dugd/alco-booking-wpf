namespace AlcoBooking.Algorithms.Sorters
{
    public class QuickSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            if (items == null || items.Count <= 1) return;
            QuickSortRecursive(items, keySelector, 0, items.Count - 1);
        }

        private void QuickSortRecursive(IList<T> items, Func<T, IComparable> keySelector, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(items, keySelector, low, high);
                QuickSortRecursive(items, keySelector, low, pivotIndex - 1);
                QuickSortRecursive(items, keySelector, pivotIndex + 1, high);
            }
        }

        private int Partition(IList<T> items, Func<T, IComparable> keySelector, int low, int high)
        {
            var pivot = keySelector(items[high]);
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (keySelector(items[j]).CompareTo(pivot) <= 0)
                {
                    i++;
                    (items[i], items[j]) = (items[j], items[i]);
                }
            }

            (items[i + 1], items[high]) = (items[high], items[i + 1]);
            return i + 1;
        }
    }
}
