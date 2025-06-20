namespace AlcoBooking.Algorithms.Sorters
{
    public class InsertionSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            int n = items.Count;
            for (int i = 1; i < n; i++)
            {
                var current = items[i];
                int j = i - 1;
                while (j >= 0 && keySelector(items[j]).CompareTo(keySelector(current)) > 0)
                {
                    items[j + 1] = items[j];
                    j--;
                }
                items[j + 1] = current;
            }
        }
    }
}
