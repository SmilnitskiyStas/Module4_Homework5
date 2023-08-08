using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseLazyLoading.Entities
{
    public class Office
    {
        public int OfficeId { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
