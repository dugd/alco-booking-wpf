namespace AlcoBooking.Algorithms.Sorters
{
    public class SelectionSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            int n = items.Count;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (keySelector(items[j]).CompareTo(keySelector(items[minIndex])) < 0)
                    {
                        minIndex = j;
                    }
                }
                (items[i], items[minIndex]) = (items[minIndex], items[i]);
            }
        }
    }
}
