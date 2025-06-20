namespace AlcoBooking.Algorithms.Sorters
{
    public class ShellSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            int n = items.Count;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    var temp = items[i];
                    int j = i;
                    while (j >= gap && keySelector(items[j - gap]).CompareTo(keySelector(temp)) > 0)
                    {
                        items[j] = items[j - gap];
                        j -= gap;
                    }
                    items[j] = temp;
                }
            }
        }
    }
}
