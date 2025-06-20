namespace AlcoBooking.Algorithms.Sorters
{
    public class ShakerSorter<T> : ISorter<T>
    {
        public void Sort(IList<T> items, Func<T, IComparable> keySelector)
        {
            int left = 0, right = items.Count - 1;
            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (keySelector(items[i]).CompareTo(keySelector(items[i + 1])) > 0)
                        (items[i], items[i + 1]) = (items[i + 1], items[i]);
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    if (keySelector(items[i - 1]).CompareTo(keySelector(items[i])) > 0)
                        (items[i], items[i - 1]) = (items[i - 1], items[i]);
                }
                left++;
            }
        }
    }
}
