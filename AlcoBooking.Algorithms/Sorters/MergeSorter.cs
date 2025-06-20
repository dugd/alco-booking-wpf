namespace AlcoBooking.Algorithms.Sorters
{
    public class MergeSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            MergeSortRecursive(items, keySelector, 0, items.Count - 1);
        }

        private void MergeSortRecursive(IList<T> items, Func<T, IComparable> keySelector, int left, int right)
        {
            if (left >= right) return;

            int mid = (left + right) / 2;
            MergeSortRecursive(items, keySelector, left, mid);
            MergeSortRecursive(items, keySelector, mid + 1, right);
            Merge(items, keySelector, left, mid, right);
        }

        private void Merge(IList<T> items, Func<T, IComparable> keySelector, int left, int mid, int right)
        {
            int leftSize = mid - left + 1;
            int rightSize = right - mid;

            var leftPart = new T[leftSize];
            var rightPart = new T[rightSize];

            for (int i = 0; i < leftSize; i++)
                leftPart[i] = items[left + i];
            for (int j = 0; j < rightSize; j++)
                rightPart[j] = items[mid + 1 + j];

            int l = 0, r = 0, k = left;
            while (l < leftSize && r < rightSize)
            {
                if (keySelector(leftPart[l]).CompareTo(keySelector(rightPart[r])) <= 0)
                    items[k++] = leftPart[l++];
                else
                    items[k++] = rightPart[r++];
            }

            while (l < leftSize) items[k++] = leftPart[l++];
            while (r < rightSize) items[k++] = rightPart[r++];
        }
    }
}
