namespace AlcoBooking.Algorithms.Sorters
{
    public class BubbleSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            int n = items.Count;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (keySelector(items[j]).CompareTo(keySelector(items[j + 1])) > 0)
                    {
                        (items[j], items[j + 1]) = (items[j + 1], items[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }
    }
}
