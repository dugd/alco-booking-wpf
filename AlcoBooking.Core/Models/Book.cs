using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoBooking.Core.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title {  get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
