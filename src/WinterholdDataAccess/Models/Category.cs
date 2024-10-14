using System;
using System.Collections.Generic;

namespace WinterholdDataAccess.Models
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public string Name { get; set; } = null!;
        public int Floor { get; set; }
        public string Isle { get; set; } = null!;
        public string Bay { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
