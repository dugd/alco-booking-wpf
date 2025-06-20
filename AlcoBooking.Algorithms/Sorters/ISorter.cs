using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoBooking.Algorithms.Sorters
{
    public interface ISorter<T>
    {
        void Sort(IList<T> items, Func<T, IComparable> keySelector);
    }
}
